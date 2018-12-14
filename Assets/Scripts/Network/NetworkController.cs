using UnityEngine;
using System.Collections;
using System.IO;

namespace MultiPlayer
{
    public class NetworkController : PlayerController
    {
        GameSession Session;
        public PlayerState PlayerState;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerState==null)
                return;
            
        }
    }

}