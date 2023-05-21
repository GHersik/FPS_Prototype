using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input", menuName = "Game/Input Action Reader")]
public class InputManager : ScriptableObject, InputActions.IGameplayActions
{
    //Inform those who are interested
    public event UnityAction<Vector2> PointerMoveEvent = delegate { };
    public event UnityAction FireEvent = delegate { };
    public event UnityAction JumpEvent = delegate { };


    private InputActions inputActions;


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            FireEvent?.Invoke();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            PointerMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            JumpEvent?.Invoke();
    }

    private void DisableAllInput()
    {
        inputActions.Gameplay.Disable();
    }

    private void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions = new InputActions();

            inputActions.Gameplay.SetCallbacks(this);
        }
    }

    private void OnDisable() => DisableAllInput();
}
