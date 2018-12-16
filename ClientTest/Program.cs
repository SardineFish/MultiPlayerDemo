using System;
using MultiPlayer;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace ClientTest
{
    class Program
    {
        public const int ReceiveInterval = 16;
        static void Main(string[] args)
        {
            var client = new TCPClient();
            client.Connect("localhost", 5325);
            var lastReceive = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            while (true)
            {
                var watcher = Stopwatch.StartNew();
                for(var data = client.GetData();data!=null;  data = client.GetData())
                {
                    watcher.Stop();
                    var timestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;

                    using (var ms = new MemoryStream(data))
                    using (var br = new BinaryReader(ms))
                    {
                        var serverTime = br.ReadInt64();
                        Console.WriteLine($"Receive at {timestamp}, server-time: {serverTime}, dt: {timestamp - serverTime}, size: {data.Length}, duration: {timestamp - lastReceive}, using {watcher.Elapsed.Milliseconds}ms.");
                        lastReceive = timestamp;
                    }
                }

                Thread.Sleep(ReceiveInterval);
            }
        }
    }
}
