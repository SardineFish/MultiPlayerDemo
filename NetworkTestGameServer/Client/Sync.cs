using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer
{
    [Serializable]
    public class Sync
    {
        public int Tick;
        public PlayerState[] syncStates;
    }
}
