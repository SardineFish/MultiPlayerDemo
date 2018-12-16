using UnityEngine;
using System.Collections;

public class InputController : PlayerController
{
    public MultiPlayer.PlayerState PlayerState;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition);
        var movement = InputManager.Instance.Movement;
        var aim = InputManager.Instance.Aim;
        if (InputManager.Instance.ControlByMouse)
            aim = InputManager.Instance.MousePosition - transform.position.ToVector2();
        PlayerState = new MultiPlayer.PlayerState()
        {
            Tick = GameSystem.Instance.Tick,
            Movement = new MultiPlayer.Vec2(movement.x, movement.y),
            Aim = new MultiPlayer.Vec2(aim.x, aim.y),
            Position = new MultiPlayer.Vec2(transform.position.x, transform.position.y)
        };

        Move(movement);
        Aim(aim);
        if (InputManager.Instance.Fire)
        {
            PlayerState.Fire = true;
            Fire();
        }
    }
    
}
