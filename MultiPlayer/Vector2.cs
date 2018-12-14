using Cytar;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayer
{
    public struct Vector2
    {
        [SerializableProperty(0)]
        public float x;
        [SerializableProperty(1)]
        public float y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
