using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetworkTestGameServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServerLog.SetLogFile("./server.log");
            var server = new TCPServer("0.0.0.0", 7795);
            GameServer gameServer = new GameServer(server);
            if (args.Any(arg => arg == "mirror"))
                gameServer.Mirror = true;
            gameServer.Start();
        }
    }
}
