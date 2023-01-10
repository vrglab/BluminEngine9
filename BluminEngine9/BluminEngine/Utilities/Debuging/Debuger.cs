
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.BluminEngine.Utilities.Debuging
{
    public static class Debug
    {
        static string time { get => $"{DateTime.UtcNow.Day}/{DateTime.UtcNow.Month}/{DateTime.UtcNow.Year} {DateTime.UtcNow.Hour}:{DateTime.UtcNow.Minute}:{DateTime.UtcNow.Second}:{DateTime.UtcNow.Millisecond}"; }
        static List<string> enteries = new List<string>();
        public static void Log(object? data)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string logText = $"[SYSTEM {time}]: " + data;
            Console.WriteLine(logText);
            enteries.Add(logText);
        }

        public static void LogWarning(object? data)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string logText = $"[WARNING {time}]: " + data;
            Console.WriteLine(logText);
            enteries.Add(logText);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(object? data)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string logText = $"[ERROR {time}]: " + data;
            Console.WriteLine(logText);
            enteries.Add(logText);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogException(Exception data)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string logText = $"[EXCEPTION {time}]: " + data.Message + Environment.NewLine+data.StackTrace;
            Console.WriteLine(logText);
            enteries.Add(logText);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void OnClose()
        {
            string LogFile = AppInfo.PersistantDatapath + "\\" + "Player.log";
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-------------SYSTEM DATA-------------");
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("Working path: " + Environment.ProcessPath);
            sb.AppendLine("System name: " + Environment.MachineName);
            sb.AppendLine("OS version: " + Environment.OSVersion);
            sb.AppendLine("Is Windows: " + OperatingSystem.IsWindows());
            sb.AppendLine("Is Linux: " + OperatingSystem.IsLinux());
            sb.AppendLine("Is MacOS: " + OperatingSystem.IsMacOS());
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("-------------LOGGED DATA-------------");
            foreach (var item in enteries)
            {
                sb.AppendLine(item);
            }

            StreamWriter sw = File.CreateText(LogFile);
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();
        }
    }
}
