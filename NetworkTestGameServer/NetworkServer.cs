using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer
{
    public abstract class NetworkServer
    {
        public abstract void Start();
        public abstract NetworkSession Listen();
    }
}
