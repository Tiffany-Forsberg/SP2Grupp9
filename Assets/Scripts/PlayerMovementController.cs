using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private EntityMovement entityMovement;

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
}
