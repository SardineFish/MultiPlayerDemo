using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MultiPlayer
{
    public struct PlayerControlState
    {
        public Vector2 Position;
        public Vector2 Aim;
        public Vector2 Movement;
        public bool Fire;
    }
}
