using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static CustomInput;

[CreateAssetMenu(fileName ="Input Channel", menuName = "Channels/Input Channel", order = 0)]
public class InputChannel : ScriptableObject, IPlayerActions, IUIActions
{
    CustomInput customInput;
    private void OnEnable()
    {
        if(customInput == null)
        {
            customInput = new CustomInput();

            customInput.Player.SetCallbacks(this);
            customInput.UI.SetCallbacks(this);
            customInput.Enable();
        }
        EnablePlayer();
    }

    public void EnablePlayer()
    {
        customInput.Player.Enable();
        customInput.UI.Disable();
    }
    public void EnableUI()
    {
        customInput.UI.Enable();
        customInput.Player.Disable();
    }

    public void DisableAll()
    {
        customInput.UI.Disable();
        customInput.Player.Disable();
    }

    public Action<Vector2> MoveEvent;
    public Action ShootEvent;
    public Action ShootCancelledEvent;
    public Action PauseEvent;
    public Action PauseCancelledEvent;

    public Action<Vector2> NavigateEvent;
    public Action SubmitEvent;
    public Action SubmitCancelledEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"OnMove {context.phase} Value {context.ReadValue<Vector2>()} {context.control.device}");
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        Debug.Log($"OnShoot {context.phase}");
        if (context.phase == InputActionPhase.Performed)
        {
            ShootEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            ShootCancelledEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            PauseCancelledEvent?.Invoke();
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        NavigateEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            SubmitEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            SubmitCancelledEvent?.Invoke();
        }
    }
}
