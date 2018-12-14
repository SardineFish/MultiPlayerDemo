using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer.Client
{
    public abstract class NetworkClient
    {
        public abstract void Connect(string host, int port);
        public abstract void SendPackage<T>(T package) where T : class;
        public abstract T GetPackage<T>() where T : class;
    }
}
