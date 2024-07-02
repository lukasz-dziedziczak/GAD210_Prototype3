using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    [field: SerializeField] public Vector2 Movement { get; private set; }
    [field: SerializeField] public Vector2 Look { get; private set; }
    public event Action OnPickupPress;
    public event Action OnPickupRelease;
    public event Action OnPlaySoundPress;

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

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (context.performed) OnPickupPress?.Invoke();
        else if (context.canceled) OnPickupRelease?.Invoke();
    }

    public void OnPlaySound(InputAction.CallbackContext context)
    {
        if (context.performed) OnPlaySoundPress?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }
}
