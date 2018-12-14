using Cytar;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiPlayer
{
    public struct Vec2
    {
        [SerializableProperty(0)]
        public float x;
        [SerializableProperty(1)]
        public float y;
        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2 operator - (Vec2 v)
        {
            return new Vec2(-v.x, -v.y);
        }
        public static Vec2 operator +(Vec2 u, Vec2 v) => new Vec2(v.x + u.x, v.y + u.y);
        public static Vec2 operator -(Vec2 u, Vec2 v) => new Vec2(u.x - v.x, u.y - v.y);
        public static Vec2 operator *(Vec2 v, float k) => new Vec2(v.x * k, v.y * k);
        public static Vec2 operator /(Vec2 v, float k) => new Vec2(v.x / k, v.y / k);
        public static bool operator ==(Vec2 u, Vec2 v) => u.x == v.x && u.y == v.y;
        public static bool operator !=(Vec2 u, Vec2 v) => u.x != v.x || u.y != v.y;

        public override bool Equals(object obj)
        {
            if (!(obj is Vec2))
            {
                return false;
            }

            var vec = (Vec2)obj;
            return x == vec.x &&
                   y == vec.y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}
