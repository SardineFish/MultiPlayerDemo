using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer.Client
{
    [Serializable]
    public class Sync
    {
        public int Tick;
        public PlayerState[] syncStates;
    }
}
