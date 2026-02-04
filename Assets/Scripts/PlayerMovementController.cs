using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;
    
    [SerializeField] private GroundCheck groundCheck;

    private int _heldMovementDirection;
    private bool _jumpInputHeld = false;
    
    [SerializeField] private float inputBuffer;
    private float _inputTimer;

    private void FixedUpdate()
    {
        entityMovement.HandleHorizontalMovement(_heldMovementDirection);

        _inputTimer = Mathf.Max(_inputTimer-Time.fixedDeltaTime, 0f);
        
        if (groundCheck.IsGrounded() && _inputTimer > 0)
        {
            entityMovement.Jump(() => _jumpInputHeld || _inputTimer > 0f);
            _inputTimer = 0;
        }
    }

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        _heldMovementDirection = Math.Sign(input.x);
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jumpInputHeld = true;
            _inputTimer = inputBuffer;
        }
        if (context.canceled)
        {
            _jumpInputHeld = false;
        }
    }
}
