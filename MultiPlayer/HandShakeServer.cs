using System;
using System.Collections.Generic;
using System.Text;
using Cytar;

namespace MultiPlayer
{
    public class HandShakeServer
    {
        [SerializableProperty(0)]
        public string ID;
        [SerializableProperty(1)]
        public string Name;
        [SerializableProperty(2)]
        public PlayerData[] Players;
    }
}
