using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer
{
    public abstract class NetworkSession
    {
        public abstract bool Connected { get; }
        public abstract T GetPackage<T>() where T : class;
        public abstract void SendPackage<T>(T package);
        public abstract void SendData(byte[] data);
        public abstract byte[] ReceiveData();
    }
}
