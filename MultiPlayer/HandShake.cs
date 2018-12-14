using System;
using System.Collections.Generic;
using System.Text;
using Cytar;
namespace MultiPlayer
{
    [Serializable]
    public class HandShake
    {
        [SerializableProperty(0)]
        public string Name;
        [SerializableProperty(1)]
        public string ID;
    }
}
