using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NetworkTestGameServer
{
    public class TCPServer : NetworkServer
    {
        public string Host;
        public int Port;
        TcpListener Listener;

        public TCPServer(string host,int port)
        {
            Host = host;
            Port = port;
            IPAddress addr;
            if(host =="0.0.0.0" || host== "::0")
            {
                addr = IPAddress.Any;
            }
            else
            {
                var addresses = Dns.GetHostAddresses(host);
                if (addresses.Length <= 0)
                    throw new Exception($"Cannot resolve host '{host}'");
                addr = addresses[0];
            }
            Listener = new TcpListener(addr, port);
        }

        public override NetworkSession Listen()
        {
            return new TCPSession(Listener.AcceptTcpClient());
        }

        public override void Start()
        {
            Listener.Start();
            ServerLog.Log($"Server listening at {Host}:{Port}");
        }
    }
}
