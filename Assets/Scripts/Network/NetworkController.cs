using UnityEngine;
using System.Collections;
using System.IO;

namespace MultiPlayer
{
    public class NetworkController : PlayerController
    {
        GameSession Session;
        PlayerState lastState = null;
        UnityEngine.Vector2 accuracyPos;
        public PlayerState PlayerState;
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerState != null)
            {
                lastState = PlayerState;
                accuracyPos = new UnityEngine.Vector2(lastState.Position.x, lastState.Position.y);
            }
            PlayerState = null;
            if (lastState == null)
                return;
            var v = new Vector2(lastState.Movement.x, lastState.Movement.y) * MaxSpeed;
            accuracyPos += v * Time.deltaTime;
            var dx = accuracyPos - transform.position.ToVector2();
            var movement = Mathf.Clamp01(dx.magnitude / (MaxSpeed * Time.deltaTime)) * dx.normalized;
            Move(movement);
            Aim(new Vector2(lastState.Aim.x, lastState.Aim.y));
            if (lastState.Fire)
                Fire();
        }
    }

}