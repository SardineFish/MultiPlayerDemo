using UnityEngine;
using System.Collections;

public class InputController : PlayerController
{
    
    // Update is called once per frame
    void Update()
    {
        var movement = InputManager.Instance.Movement;
        var aim = InputManager.Instance.Aim;
        Move(movement);
        Aim(aim);
        if (InputManager.Instance.Fire)
            Fire();
    }
}
