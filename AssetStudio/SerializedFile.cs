﻿using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AssetStudio
{
    public class SerializedFile
    {
        public AssetsManager assetsManager;
        public FileReader reader;
        public Game game;
        public long offset = 0;
        public string fullName;
        public string originalPath;
        public string fileName;
        public BuildType buildType;
        public List<Object> Objects;
        public Dictionary<long, Object> ObjectsDic;

        public SerializedFileHeader header;
        private byte m_FileEndianess;
        public UnityVersion unityVersion = "2.5.0f5";
        public BuildTarget m_TargetPlatform = BuildTarget.UnknownPlatform;
        private bool m_EnableTypeTree = true;
        public List<SerializedType> m_Types;
        public int bigIDEnabled = 0;
        public List<ObjectInfo> m_Objects;
        private List<LocalSerializedObjectIdentifier> m_ScriptTypes;
        public List<FileIdentifier> m_Externals;
        public List<SerializedType> m_RefTypes;
        public string userInformation;

        public SerializedFile(FileReader reader, AssetsManager assetsManager)
        {
            this.assetsManager = assetsManager;
            this.reader = reader;
            game = assetsManager.Game;
            fullName = reader.FullPath;
            fileName = reader.FileName;

            // ReadHeader
            header = new SerializedFileHeader();
            header.m_MetadataSize = reader.ReadUInt32();
            header.m_FileSize = reader.ReadUInt32();
            header.m_Version = (SerializedFileFormatVersion)reader.ReadUInt32();
            header.m_DataOffset = reader.ReadUInt32();

            if (header.m_Version >= SerializedFileFormatVersion.Unknown_9)
            {
                header.m_Endianess = reader.ReadByte();
                header.m_Reserved = reader.ReadBytes(3);
                m_FileEndianess = header.m_Endianess;
            }
            else
            {
                reader.Position = header.m_FileSize - header.m_MetadataSize;
                m_FileEndianess = reader.ReadByte();
            }
  
            if (header.m_Version >= SerializedFileFormatVersion.LargeFilesSupport)
            {
                header.m_MetadataSize = reader.ReadUInt32();
                header.m_FileSize = reader.ReadInt64();
                header.m_DataOffset = reader.ReadInt64();
                reader.ReadInt64(); // unknown

            }

            Logger.Verbose($"File {fileName} Info: {header}");

            // ReadMetadata
            if (m_FileEndianess == 0)
            {
                reader.Endian = EndianType.LittleEndian;
                Logger.Verbose($"Endianness {reader.Endian}");
            }
            if (header.m_Version >= SerializedFileFormatVersion.Unknown_7)
            {
                unityVersion = reader.ReadStringToNull();
                Logger.Verbose($"Unity version {unityVersion}");
            }
            if (header.m_Version >= SerializedFileFormatVersion.Unknown_8)
            {
                m_TargetPlatform = (BuildTarget)reader.ReadInt32();
                if (!Enum.IsDefined(typeof(BuildTarget), m_TargetPlatform))
                {
                    Logger.Verbose($"Parsed target format {m_TargetPlatform} doesn't match any of supported formats, defaulting to {BuildTarget.UnknownPlatform}");
                    m_TargetPlatform = BuildTarget.UnknownPlatform;
                }
                else if (m_TargetPlatform == BuildTarget.NoTarget && game.Type.IsMhyGroup())
                {
                    Logger.Verbose($"Selected game {game.Name} is a mhy game, forcing target format {BuildTarget.StandaloneWindows64}");
                    m_TargetPlatform = BuildTarget.StandaloneWindows64;
                }
                Logger.Verbose($"Target format {m_TargetPlatform}");
            }
            if (header.m_Version >= SerializedFileFormatVersion.HasTypeTreeHashes)
            {
                m_EnableTypeTree = reader.ReadBoolean();
            }

            // Read Types
            int typeCount = reader.ReadInt32();
            m_Types = new List<SerializedType>();
            Logger.Verbose($"Found {typeCount} serialized types");
            for (int i = 0; i < typeCount; i++)
            {
                m_Types.Add(ReadSerializedType(false));
            }

            if (header.m_Version >= SerializedFileFormatVersion.Unknown_7 && header.m_Version < SerializedFileFormatVersion.Unknown_14)
            {
                bigIDEnabled = reader.ReadInt32();
            }

            // Read Objects
            int objectCount = reader.ReadInt32();
            m_Objects = new List<ObjectInfo>();
            Objects = new List<Object>();
            ObjectsDic = new Dictionary<long, Object>();
            Logger.Verbose($"Found {objectCount} objects");
            for (int i = 0; i < objectCount; i++)
            {
                var objectInfo = new ObjectInfo();
                if (bigIDEnabled != 0)
                {
                    objectInfo.m_PathID = reader.ReadInt64();
                }
                else if (header.m_Version < SerializedFileFormatVersion.Unknown_14)
                {
                    objectInfo.m_PathID = reader.ReadInt32();
                }
                else
                {
                    reader.AlignStream();
                    objectInfo.m_PathID = reader.ReadInt64();
                }

                if (header.m_Version >= SerializedFileFormatVersion.LargeFilesSupport)
                    objectInfo.byteStart = reader.ReadInt64();
                else
                    objectInfo.byteStart = reader.ReadUInt32();

                objectInfo.byteStart += header.m_DataOffset;
                objectInfo.byteSize = reader.ReadUInt32();
                objectInfo.typeID = reader.ReadInt32();
                if (header.m_Version < SerializedFileFormatVersion.RefactoredClassId)
                {
                    objectInfo.classID = reader.ReadUInt16();
                    objectInfo.serializedType = m_Types.Find(x => x.classID == objectInfo.typeID);
                }
                else
                {
                    var type = m_Types[objectInfo.typeID];
                    objectInfo.serializedType = type;
                    objectInfo.classID = type.classID;
                }
                if (header.m_Version < SerializedFileFormatVersion.HasScriptTypeIndex)
                {
                    objectInfo.isDestroyed = reader.ReadUInt16();
                }
                if (header.m_Version >= SerializedFileFormatVersion.HasScriptTypeIndex && header.m_Version < SerializedFileFormatVersion.RefactorTypeData)
                {
                    var m_ScriptTypeIndex = reader.ReadInt16();
                    if (objectInfo.serializedType != null)
                        objectInfo.serializedType.m_ScriptTypeIndex = m_ScriptTypeIndex;
                }
                if (header.m_Version == SerializedFileFormatVersion.SupportsStrippedObject || header.m_Version == SerializedFileFormatVersion.RefactoredClassId)
                {
                    objectInfo.stripped = reader.ReadByte();
                }
                Logger.Verbose($"Object Info: {objectInfo}");
                m_Objects.Add(objectInfo);
            }

            if (header.m_Version >= SerializedFileFormatVersion.HasScriptTypeIndex)
            {
                int scriptCount = reader.ReadInt32();
                Logger.Verbose($"Found {scriptCount} scripts");
                m_ScriptTypes = new List<LocalSerializedObjectIdentifier>();
                for (int i = 0; i < scriptCount; i++)
                {
                    var m_ScriptType = new LocalSerializedObjectIdentifier();
                    m_ScriptType.localSerializedFileIndex = reader.ReadInt32();
                    if (header.m_Version < SerializedFileFormatVersion.Unknown_14)
                    {
                        m_ScriptType.localIdentifierInFile = reader.ReadInt32();
                    }
                    else
                    {
                        reader.AlignStream();
                        m_ScriptType.localIdentifierInFile = reader.ReadInt64();
                    }
                    Logger.Verbose($"Script Info: {m_ScriptType}");
                    m_ScriptTypes.Add(m_ScriptType);
                }
            }

            int externalsCount = reader.ReadInt32();
            m_Externals = new List<FileIdentifier>();
            Logger.Verbose($"Found {externalsCount} externals");
            for (int i = 0; i < externalsCount; i++)
            {
                var m_External = new FileIdentifier();
                if (header.m_Version >= SerializedFileFormatVersion.Unknown_6)
                {
                    var tempEmpty = reader.ReadStringToNull();
                }
                if (header.m_Version >= SerializedFileFormatVersion.Unknown_5)
                {
                    m_External.guid = new Guid(reader.ReadBytes(16));
                    m_External.type = reader.ReadInt32();
                }
                m_External.pathName = reader.ReadStringToNull();
                m_External.fileName = Path.GetFileName(m_External.pathName);
                Logger.Verbose($"External Info: {m_External}");
                m_Externals.Add(m_External);
            }

            if (header.m_Version >= SerializedFileFormatVersion.SupportsRefObject)
            {
                int refTypesCount = reader.ReadInt32();
                m_RefTypes = new List<SerializedType>();
                Logger.Verbose($"Found {refTypesCount} reference types");
                for (int i = 0; i < refTypesCount; i++)
                {
                    m_RefTypes.Add(ReadSerializedType(true));
                }
            }

            if (header.m_Version >= SerializedFileFormatVersion.Unknown_5)
            {
                userInformation = reader.ReadStringToNull();
            }

            //reader.AlignStream(16);
        }

        private SerializedType ReadSerializedType(bool isRefType)
        {
            Logger.Verbose($"Attempting to parse serialized" + (isRefType ? " reference" : " ") + "type");
            var type = new SerializedType();

            type.classID = reader.ReadInt32();

            if (game.Type.IsGIGroup() && BitConverter.ToBoolean(header.m_Reserved))
            {
                Logger.Verbose($"Encoded class ID {type.classID}, decoding...");
                type.classID = DecodeClassID(type.classID);
            }

            if (header.m_Version >= SerializedFileFormatVersion.RefactoredClassId)
            {
                type.m_IsStrippedType = reader.ReadBoolean();
            }

            if (header.m_Version >= SerializedFileFormatVersion.RefactorTypeData)
            {
                type.m_ScriptTypeIndex = reader.ReadInt16();
            }

            if (header.m_Version >= SerializedFileFormatVersion.HasTypeTreeHashes)
            {
                if (isRefType && type.m_ScriptTypeIndex >= 0)
                {
                    type.m_ScriptID = reader.ReadBytes(16);
                }
                else if ((header.m_Version < SerializedFileFormatVersion.RefactoredClassId && type.classID < 0) || (header.m_Version >= SerializedFileFormatVersion.RefactoredClassId && type.classID == 114))
                {
                    type.m_ScriptID = reader.ReadBytes(16);
                }
                type.m_OldTypeHash = reader.ReadBytes(16);
            }

            if (m_EnableTypeTree)
            {
                Logger.Verbose($"File has type tree enabled !!");
                type.m_Type = new TypeTree();
                type.m_Type.m_Nodes = new List<TypeTreeNode>();
                if (header.m_Version >= SerializedFileFormatVersion.Unknown_12 || header.m_Version == SerializedFileFormatVersion.Unknown_10)
                {
                    TypeTreeBlobRead(type.m_Type);
                }
                else
                {
                    ReadTypeTree(type.m_Type);
                }
                if (header.m_Version >= SerializedFileFormatVersion.StoresTypeDependencies)
                {
                    if (isRefType)
                    {
                        type.m_KlassName = reader.ReadStringToNull();
                        type.m_NameSpace = reader.ReadStringToNull();
                        type.m_AsmName = reader.ReadStringToNull();
                    }
                    else
                    {
                        type.m_TypeDependencies = reader.ReadInt32Array();
                    }
                }
            }

            Logger.Verbose($"Serialized type info: {type}");
            return type;
        }

        private void ReadTypeTree(TypeTree m_Type, int level = 0)
        {
            Logger.Verbose($"Attempting to parse type tree...");
            var typeTreeNode = new TypeTreeNode();
            m_Type.m_Nodes.Add(typeTreeNode);
            typeTreeNode.m_Level = level;
            typeTreeNode.m_Type = reader.ReadStringToNull();
            typeTreeNode.m_Name = reader.ReadStringToNull();
            typeTreeNode.m_ByteSize = reader.ReadInt32();
            if (header.m_Version == SerializedFileFormatVersion.Unknown_2)
            {
                var variableCount = reader.ReadInt32();
            }
            if (header.m_Version != SerializedFileFormatVersion.Unknown_3)
            {
                typeTreeNode.m_Index = reader.ReadInt32();
            }
            typeTreeNode.m_TypeFlags = reader.ReadInt32();
            typeTreeNode.m_Version = reader.ReadInt32();
            if (header.m_Version != SerializedFileFormatVersion.Unknown_3)
            {
                typeTreeNode.m_MetaFlag = reader.ReadInt32();
            }

            int childrenCount = reader.ReadInt32();
            for (int i = 0; i < childrenCount; i++)
            {
                ReadTypeTree(m_Type, level + 1);
            }

            Logger.Verbose($"Type Tree Info: {m_Type}");
        }

        private void TypeTreeBlobRead(TypeTree m_Type)
        {
            Logger.Verbose($"Attempting to parse blob type tree...");
            int numberOfNodes = reader.ReadInt32();
            int stringBufferSize = reader.ReadInt32();
            Logger.Verbose($"Found {numberOfNodes} nodes and {stringBufferSize} strings");
            for (int i = 0; i < numberOfNodes; i++)
            {
                var typeTreeNode = new TypeTreeNode();
                m_Type.m_Nodes.Add(typeTreeNode);
                typeTreeNode.m_Version = reader.ReadUInt16();
                typeTreeNode.m_Level = reader.ReadByte();
                typeTreeNode.m_TypeFlags = reader.ReadByte();
                typeTreeNode.m_TypeStrOffset = reader.ReadUInt32();
                typeTreeNode.m_NameStrOffset = reader.ReadUInt32();
                typeTreeNode.m_ByteSize = reader.ReadInt32();
                typeTreeNode.m_Index = reader.ReadInt32();
                typeTreeNode.m_MetaFlag = reader.ReadInt32();
                if (header.m_Version >= SerializedFileFormatVersion.TypeTreeNodeWithTypeFlags)
                {
                    typeTreeNode.m_RefTypeHash = reader.ReadUInt64();
                }
            }
            m_Type.m_StringBuffer = reader.ReadBytes(stringBufferSize);

            using (var stringBufferReader = new EndianBinaryReader(new MemoryStream(m_Type.m_StringBuffer), EndianType.LittleEndian))
            {
                for (int i = 0; i < numberOfNodes; i++)
                {
                    var m_Node = m_Type.m_Nodes[i];
                    m_Node.m_Type = ReadString(stringBufferReader, m_Node.m_TypeStrOffset);
                    m_Node.m_Name = ReadString(stringBufferReader, m_Node.m_NameStrOffset);
                }
            }

            Logger.Verbose($"Type Tree Info: {m_Type}");

            string ReadString(EndianBinaryReader stringBufferReader, uint value)
            {
                var isOffset = (value & 0x80000000) == 0;
                if (isOffset)
                {
                    stringBufferReader.BaseStream.Position = value;
                    return stringBufferReader.ReadStringToNull();
                }
                var offset = value & 0x7FFFFFFF;
                if (CommonString.StringBuffer.TryGetValue(offset, out var str))
                {
                    return str;
                }
                return offset.ToString();
            }
        }

        public void AddObject(Object obj)
        {
            Logger.Verbose($"Caching object with {obj.m_PathID} in file {fileName}...");
            Objects.Add(obj);
            ObjectsDic.Add(obj.m_PathID, obj);
        }

        private static int DecodeClassID(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            value = BitConverter.ToInt32(bytes, 0);
            return (value ^ 0x23746FBE) - 3;
        }

        public bool IsVersionStripped => unityVersion == strippedVersion;

        private const string strippedVersion = "0.0.0";
    }
}
