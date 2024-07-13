using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    [field: SerializeField] public Vector2 Movement { get; private set; }
    [field: SerializeField] public Vector2 Look { get; private set; }
    public event Action OnLeftPress;
    public event Action OnLeftRelease;
    public event Action OnRightPress;
    public event Action OnPausePress;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.AddCallbacks(this);
        controls.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) OnPausePress?.Invoke();
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed) OnLeftPress?.Invoke();
        else if (context.canceled) OnLeftRelease?.Invoke();
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed) OnRightPress?.Invoke();
    }
}
