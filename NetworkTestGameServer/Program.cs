using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer
{
    public class Program
    {
        public static void Main()
        {
            ServerLog.SetLogFile("./server.log");
            var server = new TCPServer("0.0.0.0", 7795);
            GameServer gameServer = new GameServer(server);
            gameServer.Start();
        }
    }
}
