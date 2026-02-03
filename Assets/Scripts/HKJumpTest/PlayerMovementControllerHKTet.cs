using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementControllerHKTet : MonoBehaviour
{
    [SerializeField] private EntityMovementHKTet entityMovement;
    
    [SerializeField] private GroundCheck groundCheck;

    private int _heldMovementDirection;
    private bool _jumpInputHeld = false;
    

    private void FixedUpdate()
    {
        entityMovement.HandleHorizontalMovement(_heldMovementDirection);
    }

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        _heldMovementDirection = Math.Sign(input.x);
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (groundCheck.CheckGrounded() && context.started)
        {
            _jumpInputHeld = true;
            StartCoroutine(entityMovement.Jump(() => _jumpInputHeld));
        }

        if (context.canceled)
        {
            _jumpInputHeld = false;
        }
    }
}
