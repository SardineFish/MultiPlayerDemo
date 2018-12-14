using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTestGameServer.Client
{
    [Serializable]
    public class PlayerState
    {
        public string ID;
        public int Tick;
        public Vector2 Position;
        public Vector2 Movement;
        public Vector2 Aim;
        public bool Fire;
    }
}
