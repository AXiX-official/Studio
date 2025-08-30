using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AssetStudio
{
    public static class OodleHelper
    {
        [DllImport(@"oo2core_9_win64.dll")]
        static extern int OodleLZ_Decompress(ref byte compressedBuffer, int compressedBufferSize, ref byte decompressedBuffer, int decompressedBufferSize, int fuzzSafe, int checkCRC, int verbosity, IntPtr rawBuffer, int rawBufferSize, IntPtr fpCallback, IntPtr callbackUserData, IntPtr decoderMemory, IntPtr decoderMemorySize, int threadPhase);

        public static int Decompress(Span<byte> compressed, Span<byte> decompressed)
        {
            int numWrite = -1;
            try
            {
                numWrite = OodleLZ_Decompress(ref compressed[0], compressed.Length, ref decompressed[0], decompressed.Length, 1, 0, 0, 0, 0, 0, 0, 0, 0, 3);
            }
            catch (Exception)
            {
                throw new IOException($"Oodle解压出错, write {numWrite} bytes but expected {decompressed.Length} bytes");
            }

            return numWrite;
        }
    }
    public class Blb3File
    {
        // 定义缺少的成员
        public byte[] Header;
        private static string ModuleName = "UnityPlayer.dll";
        private List<BundleFile.StorageBlock> m_BlocksInfo;
        private List<BundleFile.Node> m_DirectoryInfo;
        public BundleFile.Header m_Header;
        public long Offset;
        public List<StreamFile> fileList;
        private static Blb3File.DecryptBlb3 _decrypt;

        // 定义委托类型
        private delegate int DecryptBlb3(ref byte buffer, ulong size, ref byte header, ulong headerSize, uint idk);

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public static void Decrypt(byte[] header, Span<byte> buffer)
        {
            if (Blb3File._decrypt == null)
            {
                IntPtr module = Blb3File.LoadLibrary(Blb3File.ModuleName);
                byte[] key = new byte[]
                {
                    99, 125, 117, 120, 246, 110, 105, 194, 56, 8,
                    109, 32, 242, 218, 165, 121, 218, 147, 219, 110,
                    238, 76, 81, 231, 181, 205, 184, 180, 128, 185,
                    108, 223, 151, 220, 177, 5, 18, 26, 209, 235,
                    28, 140, 207, 218, 93, 245, 31, 58, 52, 246,
                    17, 240, 44, 163, 51, 173, 63, 43, 186, 217,
                    215, 26, 140, 74, 73, 194, 110, 89, 95, 43,
                    28, 231, 26, 114, 156, 248, 101, 174, 97, 203,
                    3, 128, 82, 190, 116, 169, 231, 12, 50, 146,
                    228, 98, 22, 17, 6, 144, 176, 142, 200, 152,
                    39, 40, 85, 226, 45, 144, 104, 20, 60, 81,
                    241, 199, 33, 210, 50, 252, 230, 232, 78, 130,
                    196, 207, 160, 90, 108, 130, 141, 173, 77, 141,
                    145, 111, 219, 18, 194, 144, 76, 46, 244, 182,
                    232, 208, 151, 252, 240, 16, 221, 79, 182, 191,
                    6, 31, 222, 119, 34, 143, 66, 195, 149, 68,
                    64, 147, 152, 169, 237, 163, 130, 251, 106, 122,
                    6, 201, 61, 56, 74, 214, 87, 121, 133, 222,
                    57, 96, 248, 30, 212, 239, 78, 81, 217, 199,
                    16, 183, 122, 185, 231, 237, 216, 99, 114, 1,
                    32, 20, 190, 212, 135, 112, 69, 69, 160, 239,
                    103, 181, 156, 214, 32, 217, 185, 236, 141, 98,
                    90, 28, 195, 65, 1, 25, 122, 242, 141, 60,
                    104, 115, 115, 247, 109, 2, 34, 184, 198, 48,
                    124, 80, 123, 254, 75, 19, 180, 159, 185, 96,
                    215, 244, 76, 169, 69, 233
                };
                byte[] key2 = new byte[]
                {
                    41, 35, 190, 132, 225, 108, 214, 174, 82, 144,
                    73, 241, 241, 187, 233, 235, 179, 166, 219, 60,
                    135, 12, 62, 153, 36, 94, 13, 28, 6, 183,
                    71, 222, 179, 18, 77, 200, 67, 187, 139, 166,
                    31, 3, 90, 125, 9, 56, 37, 31, 93, 212,
                    203, 252, 150, 245, 69, 59, 19, 13, 137, 10,
                    28, 219, 174, 50, 32, 154, 80, 238, 64, 120,
                    54, 253, 18, 73, 50, 246, 158, 125, 73, 220,
                    173, 79, 20, 242, 68, 64, 102, 208, 107, 196,
                    48, 183, 50, 59, 161, 34, 246, 34, 145, 157,
                    225, 139, 31, 218, 176, 202, 153, 2, 185, 114,
                    157, 73, 44, 128, 126, 197, 153, 213, 233, 128,
                    178, 234, 201, 204, 83, 191, 103, 214, 191, 20,
                    214, 126, 45, 220, 142, 102, 131, 239, 87, 73,
                    97, byte.MaxValue, 105, 143, 97, 205, 209, 30, 157, 156,
                    22, 114, 114, 230, 29, 240, 132, 79, 74, 119,
                    2, 215, 232, 57, 44, 83, 203, 201, 18, 30,
                    51, 116, 158, 12, 244, 213, 212, 159, 212, 164,
                    89, 126, 53, 207, 50, 34, 244, 204, 207, 211,
                    144, 45, 72, 211, 143, 117, 230, 217, 29, 42,
                    229, 192, 247, 43, 120, 129, 135, 68, 14, 95,
                    80, 0, 212, 97, 141, 190, 123, 5, 21, 7,
                    59, 51, 130, 31, 24, 112, 146, 218, 100, 84,
                    206, 177, 133, 62, 105, 21, 248, 70, 106, 4,
                    150, 115, 14, 217, 22, 47, 103, 104, 212, 247,
                    74, 74, 208, 87, 104, 118
                };
                IntPtr key1Ptr = module + (IntPtr)33517712;
                IntPtr intPtr = module + (IntPtr)33517456;
                OverrideBytes(key1Ptr, key, 256);
                OverrideBytes(intPtr, key2, 256);
                Blb3File._decrypt = Marshal.GetDelegateForFunctionPointer<Blb3File.DecryptBlb3>(module + (IntPtr)1034032);
            }
            ulong header_size = (ulong)((long)header.Length);
            ulong buffer_size = (ulong)((long)buffer.Length);
            Blb3File._decrypt(ref buffer[0], (buffer_size > 128UL) ? 128UL : buffer_size, ref header[0], header_size, 0U);
        }


        public Blb3File(FileReader reader, string path)
        {
            this.Offset = reader.Position;
            reader.Endian = EndianType.LittleEndian;
            string signature = reader.ReadStringToNull(4);
            Logger.Verbose("解析签名" + signature);
            if (signature != "Blb\u0003")
            {
                throw new Exception("不是有效的Blb3文件");
            }
            uint size = reader.ReadUInt32();
            this.m_Header = new BundleFile.Header
            {
                version = 6U,
                unityVersion = "5.x.x",
                unityRevision = "2017.4.30f1",
                flags = (ArchiveFlags)0
            };
            this.m_Header.compressedBlocksInfoSize = size;
            this.m_Header.uncompressedBlocksInfoSize = size;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
            defaultInterpolatedStringHandler.AppendLiteral("Header: ");
            defaultInterpolatedStringHandler.AppendFormatted<BundleFile.Header>(this.m_Header);
            Logger.Verbose(defaultInterpolatedStringHandler.ToStringAndClear());
            reader.ReadUInt32();
            this.Header = reader.ReadBytes(16);
            byte[] header = reader.ReadBytes((int)this.m_Header.compressedBlocksInfoSize);
            Blb3File.Decrypt(this.Header, header);
            ReadBlocksInfoAndDirectory(header);
            using (Stream blocksStream = CreateBlocksStream(path))
            {
                ReadBlocks(reader, blocksStream);
                ReadFiles(blocksStream, path);
            }
        }

        private static void OverrideBytes(IntPtr address, byte[] bytes, int length)
        {
            uint oldprotect;
            Blb3File.VirtualProtect(address, (uint)length, 64U, out oldprotect);
            Marshal.Copy(bytes, 0, address, length);
            Blb3File.VirtualProtect(address, (uint)length, oldprotect, out oldprotect);
        }

        private void ReadBlocksInfoAndDirectory(byte[] header)
        {
            using (MemoryStream stream = new MemoryStream(header))
            {
                using (EndianBinaryReader reader = new EndianBinaryReader(stream, EndianType.LittleEndian, false))
                {
                    this.m_Header.size = (long)((ulong)reader.ReadUInt32());
                    uint lastUncompressedSize = reader.ReadUInt32();
                    reader.Position += 4L;
                    reader.ReadInt32();
                    reader.ReadUInt32();
                    CompressionType compressionType = (CompressionType)reader.ReadByte();
                    uint uncompressedSize = 1U << (int)reader.ReadByte();
                    reader.AlignStream();
                    int blocksInfoCount = reader.ReadInt32();
                    int nodesCount = reader.ReadInt32();
                    long blocksInfoOffset = reader.Position + reader.ReadInt64();
                    long nodesInfoOffset = reader.Position + reader.ReadInt64();
                    long flagInfoOffset = reader.Position + reader.ReadInt64();
                    reader.Position = blocksInfoOffset;
                    this.m_BlocksInfo = new List<BundleFile.StorageBlock>();
                    DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
                    defaultInterpolatedStringHandler.AppendLiteral("块数量: ");
                    defaultInterpolatedStringHandler.AppendFormatted<int>(blocksInfoCount);
                    Logger.Verbose(defaultInterpolatedStringHandler.ToStringAndClear());
                    for (int i = 0; i < blocksInfoCount; i++)
                    {
                        this.m_BlocksInfo.Add(new BundleFile.StorageBlock
                        {
                            compressedSize = reader.ReadUInt32(),
                            uncompressedSize = ((i == blocksInfoCount - 1) ? lastUncompressedSize : uncompressedSize),
                            flags = (StorageBlockFlags)compressionType
                        });
                        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 2);
                        defaultInterpolatedStringHandler.AppendLiteral("块 ");
                        defaultInterpolatedStringHandler.AppendFormatted<int>(i);
                        defaultInterpolatedStringHandler.AppendLiteral("信息: ");
                        defaultInterpolatedStringHandler.AppendFormatted<BundleFile.StorageBlock>(this.m_BlocksInfo[i]);
                        Logger.Verbose(defaultInterpolatedStringHandler.ToStringAndClear());
                    }
                    for (int j = this.m_BlocksInfo.Count - 1; j > 0; j--)
                    {
                        this.m_BlocksInfo[j].compressedSize -= this.m_BlocksInfo[j - 1].compressedSize;
                        this.m_BlocksInfo[j].flags = (StorageBlockFlags)((this.m_BlocksInfo[j].compressedSize == this.m_BlocksInfo[j].uncompressedSize) ? CompressionType.None : compressionType);
                    }
                    reader.Position = nodesInfoOffset;
                    this.m_DirectoryInfo = new List<BundleFile.Node>();
                    defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
                    defaultInterpolatedStringHandler.AppendLiteral("目录数量: ");
                    defaultInterpolatedStringHandler.AppendFormatted<int>(nodesCount);
                    Logger.Verbose(defaultInterpolatedStringHandler.ToStringAndClear());
                    for (int k = 0; k < nodesCount; k++)
                    {
                        this.m_DirectoryInfo.Add(new BundleFile.Node
                        {
                            offset = (long)reader.ReadInt32(),
                            size = (long)reader.ReadInt32()
                        });
                        long pos = reader.Position;
                        reader.Position = flagInfoOffset;
                        uint flag = reader.ReadUInt32();
                        if (k >= 32)
                        {
                            flag = reader.ReadUInt32();
                        }
                        this.m_DirectoryInfo[k].flags = (uint)((ulong)flag & (ulong)(1L << (k & 31))) * 4U;
                        reader.Position = pos;
                        long pathOffset = reader.Position + reader.ReadInt64();
                        pos = reader.Position;
                        reader.Position = pathOffset;
                        this.m_DirectoryInfo[k].path = reader.ReadStringToNull(32767);
                        reader.Position = pos;
                        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
                        defaultInterpolatedStringHandler.AppendLiteral("目录 ");
                        defaultInterpolatedStringHandler.AppendFormatted<int>(k);
                        defaultInterpolatedStringHandler.AppendLiteral("信息: ");
                        defaultInterpolatedStringHandler.AppendFormatted<BundleFile.Node>(this.m_DirectoryInfo[k]);
                        Logger.Verbose(defaultInterpolatedStringHandler.ToStringAndClear());
                    }
                }
            }
        }

        private Stream CreateBlocksStream(string path)
        {
            Stream blocksStream;
            var uncompressedSizeSum = (int)m_BlocksInfo.Sum(x => x.uncompressedSize);
            Logger.Verbose($"解压块的总大小: 0x{uncompressedSizeSum:X8}");
            if (uncompressedSizeSum >= int.MaxValue)
                blocksStream = new FileStream(path + ".temp", FileMode.Create, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose);
            else
                blocksStream = new MemoryStream(uncompressedSizeSum);
            return blocksStream;
        }

        private void ReadBlocks(FileReader reader, Stream blocksStream)
        {
            foreach (var blockInfo in m_BlocksInfo)
            {
                var compressionType = (CompressionType)(blockInfo.flags & StorageBlockFlags.CompressionTypeMask);
                Logger.Verbose($"块压缩类型{compressionType}");
                switch (compressionType) //kStorageBlockCompressionTypeMask
                {
                    case CompressionType.None: //None
                        {
                            reader.BaseStream.CopyTo(blocksStream, blockInfo.compressedSize);
                            break;
                        }
                    case CompressionType.Oodle: //Oodle
                        {
                            var compressedSize = (int)blockInfo.compressedSize;
                            var uncompressedSize = (int)blockInfo.uncompressedSize;

                            var compressedBytes = ArrayPool<byte>.Shared.Rent(compressedSize);
                            var uncompressedBytes = ArrayPool<byte>.Shared.Rent(uncompressedSize);

                            var compressedBytesSpan = compressedBytes.AsSpan(0, compressedSize);
                            var uncompressedBytesSpan = uncompressedBytes.AsSpan(0, uncompressedSize);

                            try
                            {

                                reader.Read(compressedBytesSpan);

                                if (compressedSize > 6)
                                    Decrypt(Header, compressedBytesSpan);

                                var numWrite = OodleHelper.Decompress(compressedBytesSpan, uncompressedBytesSpan);
                                if (numWrite != uncompressedSize)
                                {
                                    Logger.Warning($"Oodle解压出错, write {numWrite} bytes but expected {uncompressedSize} bytes");
                                }
                            }
                            finally
                            {
                                blocksStream.Write(uncompressedBytesSpan);
                                ArrayPool<byte>.Shared.Return(compressedBytes, true);
                                ArrayPool<byte>.Shared.Return(uncompressedBytes, true);
                            }
                            break;
                        }
                    case CompressionType.Lzma: //LZMA
                        {
                            SevenZipHelper.StreamDecompress(reader.BaseStream, blocksStream, blockInfo.compressedSize, blockInfo.uncompressedSize);
                            break;
                        }
                    case CompressionType.Lz4: //LZ4
                    case CompressionType.Lz4HC: //LZ4HC
                        {
                            var compressedSize = (int)blockInfo.compressedSize;
                            var uncompressedSize = (int)blockInfo.uncompressedSize;

                            var compressedBytes = ArrayPool<byte>.Shared.Rent(compressedSize);
                            var uncompressedBytes = ArrayPool<byte>.Shared.Rent(uncompressedSize);

                            var compressedBytesSpan = compressedBytes.AsSpan(0, compressedSize);
                            var uncompressedBytesSpan = uncompressedBytes.AsSpan(0, uncompressedSize);
                            try
                            {
                                reader.Read(compressedBytesSpan);
                                Decrypt(Header, compressedBytesSpan);
                                var numWrite = LZ4.Instance.Decompress(compressedBytesSpan, uncompressedBytesSpan);
                                if (numWrite != uncompressedSize)
                                {
                                    throw new IOException($"Lz4解压出错, write {numWrite} bytes but expected {uncompressedSize} bytes");
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Error($"Lz4解压出错{e.Message}");
                            }
                            finally
                            {
                                blocksStream.Write(uncompressedBytesSpan);
                                ArrayPool<byte>.Shared.Return(compressedBytes, true);
                                ArrayPool<byte>.Shared.Return(uncompressedBytes, true);
                            }
                            break;
                        }
                    default:
                        throw new IOException($"不支持的压缩类型{compressionType}");
                }
            }
        }

        private void ReadFiles(Stream blocksStream, string path)
        {
            Logger.Verbose($"从块序列写入文件...");

            fileList = new List<StreamFile>();
            for (int i = 0; i < m_DirectoryInfo.Count; i++)
            {
                var node = m_DirectoryInfo[i];
                var file = new StreamFile();
                fileList.Add(file);
                file.path = node.path;
                file.fileName = Path.GetFileName(node.path);
                if (node.size >= int.MaxValue)
                {
                    var extractPath = path + "_unpacked" + Path.DirectorySeparatorChar;
                    Directory.CreateDirectory(extractPath);
                    file.stream = new FileStream(extractPath + file.fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                }
                else
                    file.stream = new MemoryStream((int)node.size);
                blocksStream.Position = node.offset;
                blocksStream.CopyTo(file.stream, node.size);
                file.stream.Position = 0;
            }
        }
    }
}