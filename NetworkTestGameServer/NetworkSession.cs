using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer
{
    public abstract class NetworkSession
    {
        public abstract T GetPackage<T>() where T : class;
        public abstract void SendPackage<T>();
    }
}
