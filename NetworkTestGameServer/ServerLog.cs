﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace NetworkTestGameServer
{
    public static class ServerLog
    {
        static string logPath = "server.log";
        public static void SetLogFile(string path)
        {
            logPath = path;
        }

        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.Now.ToString()}]");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Log]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{message}");
            lock (logPath)
            {
                File.AppendAllLines(logPath, new string[] { $"[{DateTime.Now.ToString()}][Log]{message}" });
            }
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.Now.ToString()}]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Warn]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{message}");
            lock (logPath)
            {
                File.AppendAllLines(logPath, new string[] { $"[{DateTime.Now.ToString()}][Warn]{message}" });
            }
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.Now.ToString()}]");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[Error]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{message}");
            lock (logPath)
            {
                File.AppendAllLines(logPath, new string[] { $"[{DateTime.Now.ToString()}][Error]{message}" });
            }
        }
    }
}
