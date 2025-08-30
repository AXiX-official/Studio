using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AssetStudio
{
    public static class Logger
    {
        private static bool _fileLogging;

        public static ILogger Default = new DummyLogger();
        public static ILogger File;

        public static bool Silent { get; set; }
        public static LoggerEvent Flags { get; set; }

        public static bool FileLogging
        {
            get => _fileLogging;
            set
            {
                _fileLogging = value;
                if (_fileLogging)
                {
                    try
                    {
                        File = new FileLogger();
                    }
                    catch
                    {
                        _fileLogging = false;
                        Error("log file is already in use, disabling...");
                        return;
                    }
                }
                else
                {
                    ((FileLogger)File)?.Dispose();
                    File = null;
                }
            }
        }

        public static void Verbose(string message)
        {
            if (!Flags.HasFlag(LoggerEvent.详细) || Silent)
                return;

            try
            {
                var callerMethod = new StackTrace().GetFrame(1).GetMethod();
                var callerMethodClass = callerMethod.ReflectedType.Name;
                if (!string.IsNullOrEmpty(callerMethodClass))
                {
                    message = $"[{callerMethodClass}] {message}";
                }
            }
            catch (Exception) { }
            if (FileLogging) File.Log(LoggerEvent.详细, message);
            Default.Log(LoggerEvent.详细, message);
        }
        public static void Debug(string message)
        {
            if (!Flags.HasFlag(LoggerEvent.调试) || Silent)
                return;

            if (FileLogging) File.Log(LoggerEvent.调试, message);
            Default.Log(LoggerEvent.调试, message);
        }
        public static void Info(string message)
        {
            if (!Flags.HasFlag(LoggerEvent.信息) || Silent)
                return;

            if (FileLogging) File.Log(LoggerEvent.信息, message);
            Default.Log(LoggerEvent.信息, message);
        }
        public static void Warning(string message)
        {
            if (!Flags.HasFlag(LoggerEvent.警告) || Silent)
                return;

            if (FileLogging) File.Log(LoggerEvent.警告, message);
            Default.Log(LoggerEvent.警告, message);
        }
        public static void Error(string message)
        {
            if (!Flags.HasFlag(LoggerEvent.错误) || Silent)
                return;

            if (FileLogging) File.Log(LoggerEvent.错误, message);
            Default.Log(LoggerEvent.错误, message);
        }

        public static void Error(string message, Exception e)
        {
            if (!Flags.HasFlag(LoggerEvent.错误) || Silent)
                return;

            var sb = new StringBuilder();
            sb.AppendLine(message);
            sb.AppendLine(e.ToString());

            message = sb.ToString();
            if (FileLogging) File.Log(LoggerEvent.错误, message);
            Default.Log(LoggerEvent.错误, message);
        }
    }
}
