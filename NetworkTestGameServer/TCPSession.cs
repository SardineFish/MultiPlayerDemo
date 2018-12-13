using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace NetworkTestGameServer
{
    public class TCPSession
    {
        TcpClient Client;

        public TCPSession(TcpClient client)
        {
            this.Client = client;
        }
    }
}
