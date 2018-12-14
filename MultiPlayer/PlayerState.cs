using System;
using System.Collections.Generic;
using System.Text;
using Cytar;
namespace MultiPlayer
{
    [Serializable]
    public class PlayerState
    {
        [SerializableProperty(0)]
        public string ID;
        [SerializableProperty(1)]
        public int Tick;
        [SerializableProperty(2)]
        public Vector2 Position;
        [SerializableProperty(3)]
        public Vector2 Movement;
        [SerializableProperty(4)]
        public Vector2 Aim;
        [SerializableProperty(5)]
        public bool Fire;
    }
}
