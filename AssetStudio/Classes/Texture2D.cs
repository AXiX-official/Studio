﻿namespace AssetStudio
{
    public class DataStreamingInfo
    {
        public uint size;
        public string path;

        public DataStreamingInfo(ObjectReader reader)
        {
            size = reader.ReadUInt32();
            path = reader.ReadAlignedString();
        }
    }
    
    public class StreamingInfo
    {
        public long offset; //ulong
        public uint size;
        public string path;

        public StreamingInfo(ObjectReader reader)
        {
            var version = reader.unityVersion;

            if (version >= "2020") //2020.1 and up
            {
                offset = reader.ReadInt64();
            }
            else
            {
                offset = reader.ReadUInt32();
            }
            size = reader.ReadUInt32();
            path = reader.ReadAlignedString();
        }
    }

    public class GLTextureSettings
    {
        public int m_FilterMode;
        public int m_Aniso;
        public float m_MipBias;
        public int m_WrapMode;

        public GLTextureSettings(ObjectReader reader)
        {
            var version = reader.unityVersion;

            m_FilterMode = reader.ReadInt32();
            m_Aniso = reader.ReadInt32();
            m_MipBias = reader.ReadSingle();
            if (reader.Game.Type.IsExAstris())
            {
                var m_TextureGroup = reader.ReadInt32();
            }
            if (version >= "2017")//2017.x and up
            {
                m_WrapMode = reader.ReadInt32(); //m_WrapU
                int m_WrapV = reader.ReadInt32();
                int m_WrapW = reader.ReadInt32();
            }
            else
            {
                m_WrapMode = reader.ReadInt32();
            }
        }
    }

    public sealed class Texture2D : Texture
    {
        public int m_Width;
        public int m_Height;
        public TextureFormat m_TextureFormat;
        public bool m_MipMap;
        public int m_MipCount;
        public GLTextureSettings m_TextureSettings;
        public ResourceReader image_data;
        public StreamingInfo m_StreamData;
        public DataStreamingInfo m_DataStreamData;

        private static bool HasGNFTexture(SerializedType type) => type.Match("1D52BB98AA5F54C67C22C39E8B2E400F");
        private static bool HasExternalMipRelativeOffset(SerializedType type) => type.Match("1D52BB98AA5F54C67C22C39E8B2E400F", "5390A985F58D5524F95DB240E8789704");
        public Texture2D(ObjectReader reader) : base(reader)
        {
            m_Width = reader.ReadInt32();
            m_Height = reader.ReadInt32();
            var m_CompleteImageSize = reader.ReadInt32();
            if (version.Major >= 2020) //2020.1 and up
            {
                var m_MipsStripped = reader.ReadInt32();
            }
            
            if (reader.IsTuanJie)
            {
                var m_WebStreaming = reader.ReadBoolean();
                reader.AlignStream();

                var m_PriorityLevel = reader.ReadInt32();
                var m_UploadedMode = reader.ReadInt32();
                m_DataStreamData = new DataStreamingInfo(reader);
            }
            
            m_TextureFormat = (TextureFormat)reader.ReadInt32();
            if (version < "5.2") //5.2 down
            {
                m_MipMap = reader.ReadBoolean();
            }
            else
            {
                m_MipCount = reader.ReadInt32();
            }
            if (version >= "2.6.0") //2.6.0 and up
            {
                var m_IsReadable = reader.ReadBoolean();
                if (reader.Game.Type.IsGI() && HasGNFTexture(reader.serializedType))
                {
                    var m_IsGNFTexture = reader.ReadBoolean();
                }
            }
            if (version.Major >= 2020) //2020.1 and up
            {
                var m_IsPreProcessed = reader.ReadBoolean();
            }
            if (version >= "2019.3") //2019.3 and up
            {
                var m_IgnoreMasterTextureLimit = reader.ReadBoolean();
            }
            if (version >= "2022.2") //2022.2 and up
            {
                reader.AlignStream(); //m_IgnoreMipmapLimit
                var m_MipmapLimitGroupName = reader.ReadAlignedString();
            }
            if (version.Major >= 3) //3.0.0 - 5.4
            {
                if (version <= "5.4")
                {
                    var m_ReadAllowed = reader.ReadBoolean();
                }
            }
            if (version >= "2018.2") //2018.2 and up
            {
                var m_StreamingMipmaps = reader.ReadBoolean();
            }
            reader.AlignStream();
            if (reader.Game.Type.IsGI() && HasGNFTexture(reader.serializedType))
            {
                var m_TextureGroup = reader.ReadInt32();
            }
            if (version >= "2018.2") //2018.2 and up
            {
                var m_StreamingMipmapsPriority = reader.ReadInt32();
            }
            var m_ImageCount = reader.ReadInt32();
            var m_TextureDimension = reader.ReadInt32();
            m_TextureSettings = new GLTextureSettings(reader);
            if (version.Major >= 3) //3.0 and up
            {
                var m_LightmapFormat = reader.ReadInt32();
            }
            if (version >= "3.5.0") //3.5.0 and up
            {
                var m_ColorSpace = reader.ReadInt32();
            }
            if (version >= "2020.2"
                || (reader.IsTuanJie && version.Major == 2022 && int.Parse(version.Extra.Substring(1)) >= 13)) //2020.2 and up
            {
                var m_PlatformBlob = reader.ReadUInt8Array();
                reader.AlignStream();
            }
            var image_data_size = reader.ReadInt32();
            if (image_data_size == 0 && (version >= "5.3.0" || reader.IsTuanJie))//5.3.0 and up
            {
                if (reader.Game.Type.IsGI() && HasExternalMipRelativeOffset(reader.serializedType))
                {
                    var m_externalMipRelativeOffset = reader.ReadUInt32();
                }
                m_StreamData = new StreamingInfo(reader);
            }

            ResourceReader resourceReader;
            if (!string.IsNullOrEmpty(m_StreamData?.path))
            {
                resourceReader = new ResourceReader(m_StreamData.path, assetsFile, m_StreamData.offset, m_StreamData.size);
            }
            else
            {
                resourceReader = new ResourceReader(reader, reader.BaseStream.Position, image_data_size);
            }
            image_data = resourceReader;
        }
    }
}