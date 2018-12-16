using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;

namespace MultiPlayer
{
    public class NetworkController : PlayerController
    {
        GameSession Session;
        public PlayerControlState PlayerState;
        public int Tick = 0;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            var v = PlayerState.Movement * MaxSpeed;
            PlayerState.Position += v * Time.deltaTime;
            var dx = PlayerState.Position - transform.position.ToVector2();
            var movement = Mathf.Clamp01(dx.magnitude / (MaxSpeed * Time.deltaTime)) * dx.normalized;
            Move(movement);
            Aim(PlayerState.Aim);
            if (PlayerState.Fire)
                Fire();
        }
    }

}