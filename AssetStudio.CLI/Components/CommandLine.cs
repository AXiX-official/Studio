using System;
using System.IO;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AssetStudio.CLI
{
    public static class CommandLine
    {
        public static void Init(string[] args)
        {
            var rootCommand = RegisterOptions();
            rootCommand.Invoke(args);
        }
        public static RootCommand RegisterOptions()
        {
            var optionsBinder = new OptionsBinder();
            var rootCommand = new RootCommand()
            {
                optionsBinder.Silent,
                optionsBinder.LoggerFlags,
                optionsBinder.TypeFilter,
                optionsBinder.NameFilter,
                optionsBinder.ContainerFilter,
                optionsBinder.GameName,
                optionsBinder.KeyIndex,
                optionsBinder.MapOp,
                optionsBinder.MapType,
                optionsBinder.MapName,
                optionsBinder.UnityVersion,
                optionsBinder.GroupAssetsType,
                optionsBinder.AssetExportType,
                optionsBinder.Key,
                optionsBinder.AIFile,
                optionsBinder.DummyDllFolder,
                optionsBinder.Input,
                optionsBinder.Output,
                optionsBinder.AutoDetectMultipleBundle,
            };

            rootCommand.SetHandler(Program.Run, optionsBinder);

            return rootCommand;
        }
    }
    public class Options
    {
        public bool Silent { get; set; }
        public LoggerEvent[] LoggerFlags { get; set; }
        public string[] TypeFilter { get; set; }
        public Regex[] NameFilter { get; set; }
        public Regex[] ContainerFilter { get; set; }
        public string GameName { get; set; }
        public int KeyIndex { get; set; }
        public MapOpType MapOp { get; set; }
        public ExportListType MapType { get; set; }
        public string MapName { get; set; }
        public string UnityVersion { get; set; }
        public AssetGroupOption GroupAssetsType { get; set; }
        public ExportType AssetExportType { get; set; }
        public byte Key { get; set; }
        public FileInfo AIFile { get; set; }
        public DirectoryInfo DummyDllFolder { get; set; }
        public FileInfo Input { get; set; }
        public DirectoryInfo Output { get; set; }
        public bool AutoDetectMultipleBundle { get; set; }
    }

    public class OptionsBinder : BinderBase<Options>
    {
        public readonly Option<bool> Silent;
        public readonly Option<LoggerEvent[]> LoggerFlags;
        public readonly Option<string[]> TypeFilter;
        public readonly Option<Regex[]> NameFilter;
        public readonly Option<Regex[]> ContainerFilter;
        public readonly Option<string> GameName;
        public readonly Option<int> KeyIndex;
        public readonly Option<MapOpType> MapOp;
        public readonly Option<ExportListType> MapType;
        public readonly Option<string> MapName;
        public readonly Option<string> UnityVersion;
        public readonly Option<AssetGroupOption> GroupAssetsType;
        public readonly Option<ExportType> AssetExportType;
        public readonly Option<byte> Key;
        public readonly Option<FileInfo> AIFile;
        public readonly Option<DirectoryInfo> DummyDllFolder;
        public readonly Argument<FileInfo> Input;
        public readonly Argument<DirectoryInfo> Output;
        public readonly Option<bool> AutoDetectMultipleBundle;

        public OptionsBinder()
        {
            Silent = new Option<bool>("--silent", "隐藏日志消息.");
            LoggerFlags = new Option<LoggerEvent[]>("--logger_flags", "用于控制切换日志事件的标志.") { AllowMultipleArgumentsPerToken = true, ArgumentHelpName = "Verbose|Debug|Info|etc.." };
            TypeFilter = new Option<string[]>("--types", "指定unity类类型") { AllowMultipleArgumentsPerToken = true, ArgumentHelpName = "Texture2D|Shader:Parse|Sprite:Both|etc.." };
            NameFilter = new Option<Regex[]>("--names", result => 
            {
                var items = new List<Regex>();
                var value = result.Tokens.Single().Value;
                if (File.Exists(value))
                {
                    var lines = File.ReadLines(value);
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        try
                        {
                            items.Add(new Regex(line, RegexOptions.IgnoreCase));
                        }
                        catch (ArgumentException e)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    items.AddRange(result.Tokens.Select(x => new Regex(x.Value, RegexOptions.IgnoreCase)).ToArray());
                }

                return items.ToArray();
            }, false, "指定名称正则表达式过滤器.") { AllowMultipleArgumentsPerToken = true };
            ContainerFilter = new Option<Regex[]>("--containers", result =>
            {
                var items = new List<Regex>();
                var value = result.Tokens.Single().Value;
                if (File.Exists(value))
                {
                    var lines = File.ReadLines(value);
                    foreach(var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        try
                        {
                            items.Add(new Regex(line, RegexOptions.IgnoreCase));
                        }
                        catch (ArgumentException e)
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    items.AddRange(result.Tokens.Select(x => new Regex(x.Value, RegexOptions.IgnoreCase)).ToArray());
                }

                return items.ToArray();
            }, false, "指定容器正则表达式过滤器.") { AllowMultipleArgumentsPerToken = true };
            GameName = new Option<string>("--游戏", $"指定游戏.") { IsRequired = true };
            KeyIndex = new Option<int>("--密钥索引", "指定密钥索引.") { ArgumentHelpName = UnityCNManager.ToString() };
            MapOp = new Option<MapOpType>("--映射", "指定要构建的映射.");
            MapType = new Option<ExportListType>("--map_type", "资源映射输出类型.");
            MapName = new Option<string>("--映射名称", () => "assets_map", "资源映射文件名.");
            UnityVersion = new Option<string>("--unity版本", "指定Unity版本.");
            GroupAssetsType = new Option<AssetGroupOption>("--资源分组", "指定导出资产应如何分组.");
            AssetExportType = new Option<ExportType>("--导出类型", "指定应如何导出资产.");
            AIFile = new Option<FileInfo>("--ai_文件", "指定资源索引json文件路径(恢复GI容器).").LegalFilePathsOnly();
            DummyDllFolder = new Option<DirectoryInfo>("--dummy_dlls", "指定DummyDll路径.").LegalFilePathsOnly();
            Input = new Argument<FileInfo>("输入路径", "输入文件/文件夹.").LegalFilePathsOnly();
            Output = new Argument<DirectoryInfo>("输出路径", "输出文件夹.").LegalFilePathsOnly();

            Key = new Option<byte>("--密钥", result =>
            {
                return ParseKey(result.Tokens.Single().Value);
            }, false, "解密米哈游二进制数据的异或密钥.");

            LoggerFlags.AddValidator(FilterValidator);
            TypeFilter.AddValidator(FilterValidator);
            NameFilter.AddValidator(FilterValidator);
            ContainerFilter.AddValidator(FilterValidator);
            Key.AddValidator(result =>
            {
                var value = result.Tokens.Single().Value;
                try
                {
                    ParseKey(value);
                }
                catch (Exception e)
                {
                    result.ErrorMessage = "无效的字节值.\n" + e.Message;
                }
            });

            GameName.FromAmong(GameManager.GetGameNames());

            LoggerFlags.SetDefaultValue(new LoggerEvent[] { LoggerEvent.调试, LoggerEvent.信息, LoggerEvent.警告, LoggerEvent.错误 });
            GroupAssetsType.SetDefaultValue(AssetGroupOption.ByType);
            AssetExportType.SetDefaultValue(ExportType.Convert);
            MapOp.SetDefaultValue(MapOpType.None);
            MapType.SetDefaultValue(ExportListType.XML);
            KeyIndex.SetDefaultValue(0);
        }
        
        public byte ParseKey(string value)
        {
            if (value.StartsWith("0x"))
            {
                value = value[2..];
                return Convert.ToByte(value, 0x10);
            }
            else
            {
                return byte.Parse(value);
            }
        }

        public void FilterValidator(OptionResult result)
        {
            var values = result.Tokens.Select(x => x.Value).ToArray();
            foreach (var val in values)
            {
                if (string.IsNullOrWhiteSpace(val))
                {
                    result.ErrorMessage = "空字符串.";
                    return;
                }

                try
                {
                    Regex.Match("", val, RegexOptions.IgnoreCase);
                }
                catch (ArgumentException e)
                {
                    result.ErrorMessage = "无效的正则表达式.\n" + e.Message;
                    return;
                }
            }
        }

        protected override Options GetBoundValue(BindingContext bindingContext) =>
        new()
        {
            Silent = bindingContext.ParseResult.GetValueForOption(Silent),
            LoggerFlags = bindingContext.ParseResult.GetValueForOption(LoggerFlags),
            TypeFilter = bindingContext.ParseResult.GetValueForOption(TypeFilter),
            NameFilter = bindingContext.ParseResult.GetValueForOption(NameFilter),
            ContainerFilter = bindingContext.ParseResult.GetValueForOption(ContainerFilter),
            GameName = bindingContext.ParseResult.GetValueForOption(GameName),
            KeyIndex = bindingContext.ParseResult.GetValueForOption(KeyIndex),
            MapOp = bindingContext.ParseResult.GetValueForOption(MapOp),
            MapType = bindingContext.ParseResult.GetValueForOption(MapType),
            MapName = bindingContext.ParseResult.GetValueForOption(MapName),
            UnityVersion = bindingContext.ParseResult.GetValueForOption(UnityVersion),
            GroupAssetsType = bindingContext.ParseResult.GetValueForOption(GroupAssetsType),
            AssetExportType = bindingContext.ParseResult.GetValueForOption(AssetExportType),
            Key = bindingContext.ParseResult.GetValueForOption(Key),
            AIFile = bindingContext.ParseResult.GetValueForOption(AIFile),
            DummyDllFolder = bindingContext.ParseResult.GetValueForOption(DummyDllFolder),
            Input = bindingContext.ParseResult.GetValueForArgument(Input),
            Output = bindingContext.ParseResult.GetValueForArgument(Output),
            AutoDetectMultipleBundle = bindingContext.ParseResult.GetValueForOption(AutoDetectMultipleBundle),
        };
    }
}
