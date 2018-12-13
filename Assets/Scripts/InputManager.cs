using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Input;

public class InputManager : Singleton<InputManager>
{
    public InputAction MovementAction;
    public InputAction AimAction;
    public InputAction FireAction;
    public Vector2 Movement;
    public Vector2 Aim;
    public bool Fire = false;
    private void OnEnable()
    {
        MovementAction.Enable();
        AimAction.Enable();
        FireAction.Enable();
        InputSystem.pollingFrequency = 120;
        MovementAction.performed += (contex) =>
        {
            Movement = contex.ReadValue<Vector2>();
        };
        AimAction.performed += (context) =>
        {
            Aim = context.ReadValue<Vector2>();
        };
        FireAction.performed += (context) =>
        {
            if (context.ReadValue<float>() > 0.5f)
            {
                Fire = true;
            }
            else
                Fire = false;
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
}
