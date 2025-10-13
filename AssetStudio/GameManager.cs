using System;
using System.Linq;
using System.Collections.Generic;
using static AssetStudio.Crypto;

namespace AssetStudio
{
    public static class GameManager
    {
        private static Dictionary<int, Game> Games = new Dictionary<int, Game>();
        static GameManager()
        {
            int index = 0;
            Games.Add(index++, new(GameType.Normal));
            Games.Add(index++, new Game(GameType.FakeHeader));
            Games.Add(index++, new(GameType.UnityCN));
            Games.Add(index++, new Game(GameType.UnityCNWithFakeHeader));
            Games.Add(index++, new Mhy(GameType.GI, GIMhyShiftRow, GIMhyKey, GIMhyMul, GIExpansionKey, GISBox, GIInitVector, GIInitSeed));
            Games.Add(index++, new Mr0k(GameType.GI_Pack, PackExpansionKey, blockKey: PackBlockKey));
            Games.Add(index++, new Mr0k(GameType.GI_CB1));
            Games.Add(index++, new Blk(GameType.GI_CB2, GI_CBXExpansionKey, initVector: GI_CBXInitVector, initSeed: GI_CBXInitSeed));
            Games.Add(index++, new Blk(GameType.GI_CB3, GI_CBXExpansionKey, initVector: GI_CBXInitVector, initSeed: GI_CBXInitSeed));
            Games.Add(index++, new Mhy(GameType.GI_CB3Pre, GI_CBXMhyShiftRow, GI_CBXMhyKey, GI_CBXMhyMul, GI_CBXExpansionKey, GI_CBXSBox, GI_CBXInitVector, GI_CBXInitSeed));
            Games.Add(index++, new Mr0k(GameType.BH3, BH3ExpansionKey, BH3SBox, BH3InitVector, BH3BlockKey));
            Games.Add(index++, new Mr0k(GameType.BH3Pre, PackExpansionKey, blockKey: PackBlockKey));
            Games.Add(index++, new Mr0k(GameType.BH3PrePre, PackExpansionKey, blockKey: PackBlockKey));
            Games.Add(index++, new Mr0k(GameType.SR_CB2, Mr0kExpansionKey, initVector: Mr0kInitVector, blockKey: Mr0kBlockKey));
            Games.Add(index++, new Mr0k(GameType.SR, Mr0kExpansionKey, initVector: Mr0kInitVector, blockKey: Mr0kBlockKey));
            Games.Add(index++, new Mr0k(GameType.ZZZ_CB1, Mr0kExpansionKey, initVector: Mr0kInitVector, blockKey: Mr0kBlockKey));
            Games.Add(index++, new Mhy(GameType.ZZZ, GIMhyShiftRow, GIMhyKey, GIMhyMul, null, GISBox, null, 0uL));
            Games.Add(index++, new Mr0k(GameType.TOT, Mr0kExpansionKey, initVector: Mr0kInitVector, blockKey: Mr0kBlockKey, postKey: ToTKey));
            Games.Add(index++, new Game("永劫无间", GameType.Naraka));
            Games.Add(index++, new Game("偶像梦幻祭2", GameType.EnsembleStars));
            Games.Add(index++, new Game("航海王:热血航线", GameType.OPFP));
            Games.Add(index++, new Game("风之幻想", GameType.FantasyOfWind));
            Games.Add(index++, new Game("胜利女神妮姬", GameType.ShiningNikki));
            Games.Add(index++, new Game("螺旋圆舞曲2:蔷薇战争", GameType.HelixWaltz2));
            Games.Add(index++, new Game(GameType.NetEase));
            Games.Add(index++, new Game("锚点降临", GameType.AnchorPanic));
            Games.Add(index++, new Game("梦间集天鹅座", GameType.DreamscapeAlbireo));
            Games.Add(index++, new Game("魔法禁书目录:幻想收束", GameType.ImaginaryFest));
            Games.Add(index++, new Game("机甲爱丽丝", GameType.AliceGearAegis));
            Games.Add(index++, new Game("世界计划多彩舞台", GameType.ProjectSekai));
            Games.Add(index++, new Game("jump群星集结", GameType.CodenameJump));
            Games.Add(index++, new Game("少女前线2:追放", GameType.GirlsFrontline));
            Games.Add(index++, new Game("重返未来1999", GameType.Reverse1999));
            Games.Add(index++, new Game("明日方舟:终末地", GameType.ArknightsEndfield));
            Games.Add(index++, new Game("咒术回战幻影夜行", GameType.JJKPhantomParade));
            Games.Add(index++, new Game("MuvLuv维度", GameType.MuvLuvDimensions));
            Games.Add(index++, new Game("动物派对", GameType.PartyAnimals));
            Games.Add(index++, new Game("恋与深空", GameType.LoveAndDeepspace));
            Games.Add(index++, new Game("学园少女突袭者", GameType.SchoolGirlStrikers));
            Games.Add(index++, new Game("来自星尘", GameType.ExAstris));
            Games.Add(index++, new Game("物华弥新", GameType.PerpetualNovelty));
            Games.Add(index++, new Game("归龙潮", GameType.GuiLongChao));
            Games.Add(index++, new Game("未来战", GameType.CounterSide));
            Games.Add(index++, new Game("新月同行", GameType.XinYueTongXing));
            Games.Add(index++, new Game("无期迷途", GameType.WuQiMiTu));
            Games.Add(index++, new Game("火影忍者", GameType.HuoYingRenZhe));
            Games.Add(index++, new Game("明日方舟",GameType.Arknights));
            Games.Add(index++, new Game("斗罗大陆:猎魂世界", GameType.LieHunShiJie));
            Games.Add(index++, new Game("魔栗少女\u2606依久乃", GameType.MagicalNutIkuno));
            Games.Add(index++, new Game("望月", GameType.WangYue));
            Games.Add(index++, new Game("崩坏学园2", GameType.GGZ));
        }
        public static Game GetGame(GameType gameType) => GetGame((int)gameType);
        public static Game GetGame(int index)
        {
            if (!Games.TryGetValue(index, out var format))
            {
                throw new ArgumentException("Invalid format !!");
            }

            return format;
        }

        public static Game GetGame(string name) => Games.FirstOrDefault(x => x.Value.Name == name).Value;
        public static Game[] GetGames() => Games.Values.ToArray();
        public static string[] GetGameNames() => Games.Values.Select(x => x.Name).ToArray();
        public static string SupportedGames() => $"Supported Games:\n{string.Join("\n", Games.Values.Select(x => x.Name))}";
    }

    public record Game
    {
        public string Name { get; set; }
        public GameType Type { get; }

        public Game(GameType type) : this(type.ToString(), type) {}
        
        public Game(string name, GameType type)
        {
            Name = name;
            Type = type;
        }

        public sealed override string ToString() => Name;
    }

    public record Mr0k : Game
    {
        public byte[] ExpansionKey { get; }
        public byte[] SBox { get; }
        public byte[] InitVector { get; }
        public byte[] BlockKey { get; }
        public byte[] PostKey { get; }

        public Mr0k(GameType type, byte[] expansionKey = null, byte[] sBox = null, byte[] initVector = null, byte[] blockKey = null, byte[] postKey = null) : base(type)
        {
            ExpansionKey = expansionKey ?? Array.Empty<byte>();
            SBox = sBox ?? Array.Empty<byte>();
            InitVector = initVector ?? Array.Empty<byte>();
            BlockKey = blockKey ?? Array.Empty<byte>();
            PostKey = postKey ?? Array.Empty<byte>();
        }
    }

    public record Blk : Game
    {
        public byte[] ExpansionKey { get; }
        public byte[] SBox { get; }
        public byte[] InitVector { get; }
        public ulong InitSeed { get; }

        public Blk(GameType type, byte[] expansionKey = null, byte[] sBox = null, byte[] initVector = null, ulong initSeed = 0) : base(type)
        {
            ExpansionKey = expansionKey ?? Array.Empty<byte>();
            SBox = sBox ?? Array.Empty<byte>();
            InitVector = initVector ?? Array.Empty<byte>();
            InitSeed = initSeed;
        }
    }

    public record Mhy : Blk
    {
        public byte[] MhyShiftRow { get; }
        public byte[] MhyKey { get; }
        public byte[] MhyMul { get; }

        public Mhy(GameType type, byte[] mhyShiftRow, byte[] mhyKey, byte[] mhyMul, byte[] expansionKey = null, byte[] sBox = null, byte[] initVector = null, ulong initSeed = 0) : base(type, expansionKey, sBox, initVector, initSeed)
        {
            MhyShiftRow = mhyShiftRow;
            MhyKey = mhyKey;
            MhyMul = mhyMul;
        }
    }

    public enum GameType
    {
        Normal,
        FakeHeader,
        UnityCN,
        UnityCNWithFakeHeader,
        GI,
        GI_Pack,
        GI_CB1,
        GI_CB2,
        GI_CB3,
        GI_CB3Pre,
        GGZ,
        BH3,
        BH3Pre,
        BH3PrePre,
        ZZZ,
        ZZZ_CB1,
        SR_CB2,
        SR,
        TOT,
        Naraka,
        EnsembleStars,
        OPFP,
        FantasyOfWind,
        ShiningNikki,
        HelixWaltz2,
        NetEase,
        AnchorPanic,
        DreamscapeAlbireo,
        ImaginaryFest,
        AliceGearAegis,
        ProjectSekai,
        CodenameJump,
        GirlsFrontline,
        Reverse1999,
        ArknightsEndfield,
        JJKPhantomParade,
        MuvLuvDimensions,
        PartyAnimals,
        LoveAndDeepspace,
        SchoolGirlStrikers,
        ExAstris,
        PerpetualNovelty,
        GuiLongChao,
        CounterSide,
        XinYueTongXing,
        Arknights,
        WuQiMiTu,
        HuoYingRenZhe,
        LieHunShiJie,
        MagicalNutIkuno,
        WangYue,
    }

    public static class GameTypes
    {
        public static bool IsNormal(this GameType type) => type == GameType.Normal;
        public static bool IsUnityCN(this GameType type) => type == GameType.UnityCN || type == GameType.GuiLongChao || type == GameType.UnityCNWithFakeHeader;
        public static bool IsGI(this GameType type) => type == GameType.GI;
        public static bool IsGIPack(this GameType type) => type == GameType.GI_Pack;
        public static bool IsGICB1(this GameType type) => type == GameType.GI_CB1;
        public static bool IsGICB2(this GameType type) => type == GameType.GI_CB2;
        public static bool IsGICB3(this GameType type) => type == GameType.GI_CB3;
        public static bool IsGICB3Pre(this GameType type) => type == GameType.GI_CB3Pre;
        public static bool IsGGZ(this GameType type) => type == GameType.GGZ;
        public static bool IsBH3(this GameType type) => type == GameType.BH3;
        public static bool IsBH3Pre(this GameType type) => type == GameType.BH3Pre;
        public static bool IsBH3PrePre(this GameType type) => type == GameType.BH3PrePre;
        public static bool IsZZZ(this GameType type) => type == GameType.ZZZ;
        public static bool IsZZZCB1(this GameType type) => type == GameType.ZZZ_CB1;
        public static bool IsSRCB2(this GameType type) => type == GameType.SR_CB2;
        public static bool IsSR(this GameType type) => type == GameType.SR;
        public static bool IsTOT(this GameType type) => type == GameType.TOT;
        public static bool IsNaraka(this GameType type) => type == GameType.Naraka;
        public static bool IsOPFP(this GameType type) => type == GameType.OPFP;
        public static bool IsNetEase(this GameType type) => type == GameType.NetEase;
        public static bool IsArknightsEndfield(this GameType type) => type == GameType.ArknightsEndfield;
        public static bool IsLoveAndDeepspace(this GameType type) => type == GameType.LoveAndDeepspace;
        public static bool IsExAstris(this GameType type) => type == GameType.ExAstris;
        public static bool IsPerpetualNovelty(this GameType type) => type == GameType.PerpetualNovelty;
        public static bool IsGuiLongChao(this GameType type) => type == GameType.GuiLongChao;
        public static bool IsCounterSide(this GameType type) => type == GameType.CounterSide;
        public static bool IsXinYueTongXing(this GameType type) => type == GameType.XinYueTongXing;
        public static bool IsArknights(this GameType type) => type == GameType.Arknights;
        public static bool IsWuQiMiTu(this GameType type) => type == GameType.WuQiMiTu;
        public static bool IsHuoYingRenZhe(this GameType type) => type == GameType.HuoYingRenZhe;
        public static bool IsLieHunShiJie(this GameType type) => type == GameType.LieHunShiJie;
        public static bool IsMagicalNutIkuno(this GameType type) => type == GameType.MagicalNutIkuno;
        public static bool IsWangYue(this GameType type) => type == GameType.WangYue;
        public static bool IsGIGroup(this GameType type) => type switch
        {
            GameType.GI or GameType.GI_Pack or GameType.GI_CB1 or GameType.GI_CB2 or GameType.GI_CB3 or GameType.GI_CB3Pre => true,
            _ => false,
        };

        public static bool IsGISubGroup(this GameType type) => type switch
        {
            GameType.GI or GameType.GI_CB2 or GameType.GI_CB3 or GameType.GI_CB3Pre => true,
            _ => false,
        };

        public static bool IsBH3Group(this GameType type) => type switch
        {
            GameType.BH3 or GameType.BH3Pre => true,
            _ => false,
        };

        public static bool IsSRGroup(this GameType type) => type switch
        {
            GameType.SR_CB2 or GameType.SR => true,
            _ => false,
        };

        public static bool IsBlockFile(this GameType type) => type switch
        {
            GameType.BH3 or GameType.BH3Pre or GameType.ZZZ or GameType.SR or GameType.GI_Pack or GameType.TOT or GameType.ArknightsEndfield or GameType.GuiLongChao or GameType.GirlsFrontline => true,
            _ => false,
        };

        public static bool IsMhyGroup(this GameType type) => type switch
        {
            GameType.GI or GameType.GI_Pack or GameType.GI_CB1 or GameType.GI_CB2 or GameType.GI_CB3 or GameType.GI_CB3Pre or GameType.BH3 or GameType.BH3Pre or GameType.BH3PrePre or GameType.SR_CB2 or GameType.SR or GameType.ZZZ or GameType.TOT => true,
            _ => false,
        };
    }
}
