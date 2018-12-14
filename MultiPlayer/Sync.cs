using Cytar;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayer
{
    [Serializable]
    public class Sync
    {
        [SerializableProperty(0)]
        public int Tick;

        [SerializableProperty(1)]
        public PlayerState[] syncStates;
    }
}
