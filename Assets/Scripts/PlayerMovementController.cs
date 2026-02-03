using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;
    
    [SerializeField] private GroundCheck groundCheck;

    private int _heldMovementInput;

    private void FixedUpdate()
    {
        entityMovement.HandleHorizontalMovement(_heldMovementInput);
    }

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        _heldMovementInput = Math.Sign(input.x);
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        Debug.Log("I'm like hey what's up hello");
        if (groundCheck.CheckGrounded() && context.performed)
        {
            entityMovement.Jump();
        }
    }
}
