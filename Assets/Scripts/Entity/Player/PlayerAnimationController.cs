using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private string yVelocityVariable;
    
    [SerializeField] private string xVelocityVariable;
    
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private string groundedBool;

    private Vector2 _direction;

    private void OnValidate()
    {
        if (!transform.parent) return;
        
        if (!animator)
        {
            Debug.LogWarning("Animator is missing in PlayerAnimationController.", this);
        }

        if (!spriteRenderer)
        {
            Debug.LogWarning("SpriteRenderer is missing in PlayerAnimationController.", this);
        }

        if (!rigidbody2D)
        {
            Debug.LogWarning("Rigidbody2D is missing in PlayerAnimationController.", this);
        }

        if (yVelocityVariable == "")
        {
            Debug.LogWarning("Y Velocity Variable is not assigned in PlayerAnimationController.", this);
        }
        
        if (xVelocityVariable == "")
        {
            Debug.LogWarning("X Velocity Variable is not assigned in PlayerAnimationController.", this);
        }
    }


    void Update()
    {
        animator.SetFloat(yVelocityVariable, rigidbody2D.linearVelocityY);
        if (_direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (_direction.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        animator.SetFloat(xVelocityVariable, Mathf.Abs(rigidbody2D.linearVelocityX));
    }

    private void FixedUpdate()
    {
        if (groundCheck.CheckGrounded())
        {
            SetBoolTrue(groundedBool);
        }
        else
        {
            SetBoolFalse(groundedBool);
        }
    }

    public void SetBoolTrue(string animatorBool)
    {
        animator.SetBool(animatorBool, true);
    }
    
    public void SetBoolFalse(string animatorBool)
    {
        animator.SetBool(animatorBool, false);
    }

    #region Trigger setters
    
    public void SetTriggerOnButtonUp(InputAction.CallbackContext context, string animatorBool)
    {
        if (context.canceled)
        {
            SetTrigger(animatorBool);
        }
    }
    
    public void SetTriggerOnButtonDown(InputAction.CallbackContext context, string animatorBool)
    {
        if (context.started)
        {
            SetTrigger(animatorBool);
        }
    }
    
    public void SetTrigger(string animatorBool)
    {
        animator.SetTrigger(animatorBool);
    }
    
    #endregion

    public void HandleLookDirection(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }
}
