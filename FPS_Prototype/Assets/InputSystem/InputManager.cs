using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input", menuName = "Game/Input Action Reader")]
public class InputManager : ScriptableObject, InputActions.IPlayerActions
{
    //Inform those who are interested
    public event UnityAction<Vector2> PointerMoveEvent = delegate { };
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction FireEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };
    public event UnityAction<Vector2> SwitchWeaponEvent = delegate { };
    public event UnityAction<bool> AimWeaponEvent = delegate { };

    private InputActions inputActions;

    public void OnPoint(InputAction.CallbackContext context) => PointerMoveEvent?.Invoke(context.ReadValue<Vector2>());
    public void OnMove(InputAction.CallbackContext context) => MoveEvent?.Invoke(context.ReadValue<Vector2>());

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            FireEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent?.Invoke();
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            SwitchWeaponEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAimWeapon(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            AimWeaponEvent?.Invoke(true);
        if (context.phase == InputActionPhase.Canceled)
            AimWeaponEvent?.Invoke(false);
    }


    public void EnableAllInput()
    {
        inputActions.Player.Enable();
    }

    public void DisableAllInput()
    {
        inputActions.Player.Disable();
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new InputActions();

            inputActions.Player.SetCallbacks(this);
        }

        //testing
        EnableAllInput();
    }

    private void OnDisable() => DisableAllInput();


}
