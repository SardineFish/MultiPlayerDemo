using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Input;

public class InputManager : Singleton<InputManager>
{
    public InputAction MovementAction;
    public InputAction AimAction;
    public InputAction FireAction;
    public InputAction MouseAction;
    public Vector2 Movement;
    public Vector2 Aim;
    public Vector2 MousePosition;
    public bool Fire = false;
    public bool ControlByMouse = false;
    private void OnEnable()
    {
        MovementAction.Enable();
        AimAction.Enable();
        FireAction.Enable();
        MouseAction.Enable();
        InputSystem.pollingFrequency = 120;
        var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MovementAction.performed += (contex) =>
        {
            Movement = contex.ReadValue<Vector2>();
        };
        AimAction.performed += (context) =>
        {
            ControlByMouse = false;
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
        MouseAction.performed += (context) =>
        {
            ControlByMouse = true;
            MousePosition = camera.ScreenToWorldPoint(context.ReadValue<Vector2>()).ToVector2();
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
}
