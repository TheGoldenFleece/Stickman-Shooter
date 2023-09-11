using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputManager : MonoBehaviour
{
    public event EventHandler<OnPlayerInputEventArgs> OnPlayerInput;
    public class OnPlayerInputEventArgs : EventArgs {
        public float touchDelta;
    }

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private bool runGameTouch;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();
        runGameTouch = false;
    }

    private void OnEnable() {
        playerInput.actions["Move"].started += OnTouchStarted;
        playerInput.actions["Move"].performed += OnTouchPerformed;
        playerInput.actions["Move"].canceled += OnTouchCanceled;
    }

    private void PlayerInputManager_started(InputAction.CallbackContext obj) {
        throw new NotImplementedException();
    }

    private void OnDisable() {
        playerInput.actions["Move"].started -= OnTouchStarted;
        playerInput.actions["Move"].performed -= OnTouchPerformed;
        playerInput.actions["Move"].canceled -= OnTouchCanceled;
    }
    private void OnTouchStarted(InputAction.CallbackContext context) {
        if (!runGameTouch) {
            GameManager.Instance.state = GameManager.State.RunStarted;
            runGameTouch = true;
        }

        OnPlayerInput?.Invoke(this, new OnPlayerInputEventArgs() {
            touchDelta = context.ReadValue<Vector2>().x
        });
    }

    private void OnTouchPerformed(InputAction.CallbackContext context) {
        OnPlayerInput?.Invoke(this, new OnPlayerInputEventArgs {
            touchDelta = context.ReadValue<Vector2>().x
        });
    }

    private void OnTouchCanceled(InputAction.CallbackContext context) {
        OnPlayerInput?.Invoke(this, new OnPlayerInputEventArgs {
            touchDelta = context.ReadValue<Vector2>().x
        });
    }

}
