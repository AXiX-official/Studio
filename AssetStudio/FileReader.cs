using System;
using System.IO;
using System.Linq;
using static AssetStudio.ImportHelper;

namespace AssetStudio
{
    public class FileReader : EndianBinaryReader
    {
        public string FullPath;
        public string FileName;
        public FileType FileType;

        private static readonly byte[] gzipMagic = new byte[] { 31, 139 };
        private static readonly byte[] brotliMagic = new byte[] { 98, 114, 111, 116, 108, 105 };
        private static readonly byte[] zipMagic = new byte[] { 80, 75, 3, 4 };
        private static readonly byte[] zipSpannedMagic = new byte[] { 80, 75, 7, 8 };
        private static readonly byte[] mhy0Magic = new byte[] { 109, 104, 121, 48 };
        private static readonly byte[] mhy1Magic = new byte[] { 109, 104, 121, 49 };
        private static readonly byte[] blb2Magic = new byte[] { 66, 108, 98, 2 };
        private static readonly byte[] blb3Magic = new byte[] { 66, 108, 98, 3 };
        private static readonly byte[] narakaMagic = new byte[] { 21, 30, 28, 13, 13, 35, 33 };
        private static readonly byte[] gunfireMagic = new byte[] { 124, 109, 121, 114, 39, 122, 115, 120, 63 };


        public FileReader(string path) : this(path, File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) { }

        public FileReader(string path, Stream stream, bool leaveOpen = false) : base(stream, EndianType.BigEndian, leaveOpen)
        {
            FullPath = Path.GetFullPath(path);
            FileName = Path.GetFileName(path);
            FileType = CheckFileType();
            Logger.Verbose($"File {path} type is {FileType}");
        }

        private FileType CheckFileType()
        {
            var signature = this.ReadStringToNull(20);
            Position = 0;
            Logger.Verbose($"Parsed signature is {signature}");
            switch (signature)
            {
                case "UnityWeb":
                case "UnityRaw":
                case "UnityArchive":
                case "UnityFS":
                    return FileType.BundleFile;
                case "UnityWebData1.0":
                    return FileType.WebFile;
                case "TuanjieWebData1.0":
                    return FileType.WebFile;
                case "blk":
                    return FileType.BlkFile;
                case "ENCR":
                    return FileType.ENCRFile;
                default:
                    {
                        Logger.Verbose("signature does not match any of the supported string signatures, attempting to check bytes signatures");
                        byte[] magic = ReadBytes(2);
                        Position = 0;
                        Logger.Verbose($"Parsed signature is {Convert.ToHexString(magic)}");
                        if (gzipMagic.SequenceEqual(magic))
                        {
                            return FileType.GZipFile;
                        }
                        Logger.Verbose($"Parsed signature does not match with expected signature {Convert.ToHexString(gzipMagic)}");
                        Position = 0x20;
                        magic = ReadBytes(6);
                        Position = 0;
                        Logger.Verbose($"Parsed signature is {Convert.ToHexString(magic)}");
                        if (brotliMagic.SequenceEqual(magic))
                        {
                            return FileType.BrotliFile;
                        }
                        Logger.Verbose($"Parsed signature does not match with expected signature {Convert.ToHexString(brotliMagic)}");
                        if (IsSerializedFile())
                        {
                            return FileType.AssetsFile;
                        }
                        magic = ReadBytes(4);
                        Position = 0;
                        Logger.Verbose($"Parsed signature is {Convert.ToHexString(magic)}");
                        if (zipMagic.SequenceEqual(magic) || zipSpannedMagic.SequenceEqual(magic))
                        {
                            return FileType.ZipFile;
                        }
                        Logger.Verbose($"Parsed signature does not match with expected signature {Convert.ToHexString(zipMagic)} or {Convert.ToHexString(zipSpannedMagic)}");
                        if (mhy0Magic.SequenceEqual(magic) || mhy1Magic.SequenceEqual(magic))
                        {
                            return FileType.MhyFile;
                        }
                        Logger.Verbose($"解析的签名与预期的签名不匹配{Convert.ToHexString(mhy0Magic)} or {Convert.ToHexString(mhy1Magic)}");
                        if (blb2Magic.SequenceEqual(magic))
                        {
                            return FileType.Blb2File;
                        }
                        Logger.Verbose($"解析的签名与预期的签名不匹配{Convert.ToHexString(blb2Magic)}");
                        if (blb3Magic.SequenceEqual(magic))
                        {
                            return FileType.Blb3File;
                        }
                        Logger.Verbose($"Parsed signature does not match with expected signature {Convert.ToHexString(mhy0Magic)}");
                        magic = ReadBytes(7);
                        Position = 0;
                        Logger.Verbose($"Parsed signature is {Convert.ToHexString(magic)}");
                        if (narakaMagic.SequenceEqual(magic))
                        {
                            return FileType.BundleFile;
                        }
                        Logger.Verbose($"Parsed signature does not match with expected signature {Convert.ToHexString(narakaMagic)}");
                        magic = ReadBytes(9);
                        Position = 0;
                        Logger.Verbose($"Parsed signature is {Convert.ToHexString(magic)}");
                        if (gunfireMagic.SequenceEqual(magic))
                        {
                            Position = 0x32;
                            return FileType.BundleFile;
                        }
                        Logger.Verbose($"Parsed signature does not match with expected signature {Convert.ToHexString(gunfireMagic)}");
                        Logger.Verbose($"Parsed signature does not match any of the supported signatures, assuming resource file");
                        return FileType.ResourceFile;
                    }
            }
        }

        private bool IsSerializedFile()
        {
            Logger.Verbose($"Attempting to check if the file is serialized file...");

            var fileSize = BaseStream.Length;
            if (fileSize < 20)
            {
                Logger.Verbose($"File size 0x{fileSize:X8} is too small, minimal acceptable size is 0x14, aborting...");
                return false;
            }
            var m_MetadataSize = ReadUInt32();
            long m_FileSize = ReadUInt32();
            var m_Version = ReadUInt32();
            long m_DataOffset = ReadUInt32();
            var m_Endianess = ReadByte();
            var m_Reserved = ReadBytes(3);
            if (m_Version >= 22)
            {
                if (fileSize < 48)
                {
                    Logger.Verbose($"File size 0x{fileSize:X8} for version {m_Version} is too small, minimal acceptable size is 0x30, aborting...");
                    Position = 0;
                    return false;
                }
                m_MetadataSize = ReadUInt32();
                m_FileSize = ReadInt64();
                m_DataOffset = ReadInt64();
            }
            Position = 0;
            if (m_FileSize != fileSize)
            {
                Logger.Verbose($"Parsed file size 0x{m_FileSize:X8} does not match stream size {fileSize}, file might be corrupted, aborting...");
                return false;
            }
            if (m_DataOffset > fileSize)
            {
                Logger.Verbose($"Parsed data offset 0x{m_DataOffset:X8} is outside the stream of the size {fileSize}, file might be corrupted, aborting...");
                return false;
            }
            Logger.Verbose($"Valid serialized file !!");
            return true;
        }
    }

    public static class FileReaderExtensions
    {
        public static FileReader PreProcessing(this FileReader reader, Game game, bool autoDetectMultipleBundle = false)
        {
            Logger.Verbose($"Applying preprocessing to file {reader.FileName}");
            if (reader.FileType == FileType.ResourceFile || !game.Type.IsNormal())
            {
                Logger.Verbose("File is encrypted !!");
                switch (game.Type)
                {
                    case GameType.GI_Pack:
                        reader = DecryptPack(reader, game);
                        break;
                    case GameType.GI_CB1:
                        reader = DecryptMark(reader);
                        break;
                    case GameType.EnsembleStars:
                        reader = DecryptEnsembleStar(reader);
                        break;
                    case GameType.OPFP:
                    case GameType.FakeHeader:
                    case GameType.UnityCNWithFakeHeader:
                    case GameType.ShiningNikki:
                        reader = ParseFakeHeader(reader);
                        break;
                    case GameType.FantasyOfWind:
                        reader = DecryptFantasyOfWind(reader);
                        break;
                    case GameType.HelixWaltz2:
                        reader = ParseHelixWaltz2(reader);
                        break;
                    case GameType.AnchorPanic:
                        reader = DecryptAnchorPanic(reader);
                        break;
                    case GameType.DreamscapeAlbireo:
                        reader = DecryptDreamscapeAlbireo(reader);
                        break;
                    case GameType.ImaginaryFest:
                        reader = DecryptImaginaryFest(reader);
                        break;
                    case GameType.AliceGearAegis:
                        reader = DecryptAliceGearAegis(reader);
                        break;
                    case GameType.ProjectSekai:
                        reader = DecryptProjectSekai(reader);
                        break;
                    case GameType.CodenameJump:
                        reader = DecryptCodenameJump(reader);
                        break;
                    case GameType.GirlsFrontline:
                        reader = DecryptGirlsFrontline(reader);
                        break; 
                    case GameType.Reverse1999:
                        reader = DecryptReverse1999(reader);
                        break;
                    case GameType.JJKPhantomParade:
                        reader = DecryptJJKPhantomParade(reader);
                        break;
                    case GameType.MuvLuvDimensions:
                        reader = DecryptMuvLuvDimensions(reader);
                        break;
                    case GameType.PartyAnimals:
                        reader = DecryptPartyAnimals(reader);
                        break;
                    case GameType.LoveAndDeepspace:
                        reader = DecryptLoveAndDeepspace(reader);
                        break;
                    case GameType.SchoolGirlStrikers:
                        reader = DecryptSchoolGirlStrikers(reader);
                        break;
                    case GameType.CounterSide:
                        reader = DecryptCounterSide(reader);
                        break;
                    case GameType.XinYueTongXing:
                        reader = DecryptXinYueTongXing(reader);
                        break;
                    case GameType.MagicalNutIkuno:
                        reader = DecryptMagicalNutIkuno(reader);
                        break;
                    case GameType.WuQiMiTu:
                        reader = DecryptWuQiMiTu(reader);
                        break;
                    case GameType.HuoYingRenZhe:
                        reader = DecryptHuoYingRenZhe(reader);
                        break;
                    case GameType.LieHunShiJie:
                        reader = DecryptLieHunShiJie(reader);
                        break;
                }
            }
            if (reader.FileType == FileType.BundleFile && game.Type.IsBlockFile() || reader.FileType == FileType.ENCRFile || reader.FileType == FileType.Blb2File || reader.FileType == FileType.Blb3File)
            {
                Logger.Verbose("File might have multiple bundles !!");
                try
                {
                    var signature = reader.ReadStringToNull();
                    reader.ReadInt32();
                    reader.ReadStringToNull();
                    reader.ReadStringToNull();
                    var size = reader.ReadInt64();
                    if (size != reader.BaseStream.Length)
                    {
                        Logger.Verbose($"Found signature {signature}, expected bundle size is 0x{size:X8}, found 0x{reader.BaseStream.Length} instead !!");
                        Logger.Verbose("Loading as block file !!");
                        reader.FileType = FileType.BlockFile;
                    }
                }
                catch (Exception) { }
                reader.Position = 0;
            }

            if (reader.FileType == FileType.MhyFile && (game.Type.IsZZZCB1() || game.Type.IsZZZ()))
            {
                reader.FileType = FileType.BlockFile;
            }
            Logger.Verbose("No preprocessing is needed");
            return reader;
        }
    } 
}
