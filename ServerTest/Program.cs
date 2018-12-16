using System;
using System.Collections.Generic;
using NetworkTestGameServer;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace ServerTest
{
    public class Program
    {
        public const int SendInterval = 100;
        public const int PackageSize = 0;
        public static List<NetworkSession> SessionList = new List<NetworkSession>();
        static void Main(string[] args)
        {
            var server = new TCPServer("0.0.0.0", 5325);
            server.Start();
            var thread = new Thread(Loop);
            thread.Start();
            while (true)
            {
                var session = server.Listen();
                lock (SessionList)
                {
                    SessionList.Add(session);
                }
                ServerLog.Log("New session started");
            }
            
        }
        static void Loop()
        {
            var random = new Random();
            while (true)
            {
                lock (SessionList)
                {
                    for (var i = 0; i < SessionList.Count; i++)
                    {
                        if (!SessionList[i].Connected)
                        {
                            SessionList.RemoveAt(i--);
                            continue;
                        }

                        var ms = new MemoryStream();
                        var bw = new BinaryWriter(ms);
                        var timestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                        bw.Write(timestamp);
                        if (PackageSize > 0)
                        {
                            var buffer = new byte[PackageSize];
                            random.NextBytes(buffer);
                        }
                        var watcher = Stopwatch.StartNew();
                        SessionList[i].SendData(ms.GetBuffer());
                        watcher.Stop();
                        Console.WriteLine($"Sent at {timestamp} using {watcher.Elapsed.TotalMilliseconds}ms");
                    }
                }

                Thread.Sleep(SendInterval);
            }
        }
    }
}
