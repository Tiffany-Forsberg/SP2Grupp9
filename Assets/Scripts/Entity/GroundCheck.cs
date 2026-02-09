using System;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    
    [Header("Settings for Raycast system")]
    [Tooltip("The size of the ground check box cast")]
    [SerializeField] private Vector2 groundCastSize;
    [SerializeField] private float castDistance;
    
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float coyoteTime;
    private CountdownTimer _coyoteTimer;

    private bool _isGrounded = false;
    

    void Start()
    {
        _coyoteTimer = new CountdownTimer(coyoteTime);
        _coyoteTimer.OnTimerStop += () => _isGrounded = false;
    }
    
    void FixedUpdate()
    {
        if (CheckGrounded())
        {
            _isGrounded = true;
            if (_coyoteTimer.IsRunning) ResetCoyoteTimer();
        }
        else
        {
            if (_isGrounded && !_coyoteTimer.IsRunning) _coyoteTimer.Start();
            if (rigidbody2D.linearVelocityY > 0)
            {
                _coyoteTimer.Reset();
                _isGrounded = false;
            }
            // Kolla rigidbody hastighet
            // Om uppåt:
            //      Reset coyote timer
            //      Sätt _isGrounded till false
            // ??
        }
    }    
    public bool IsGrounded()
    {
        return _isGrounded;
    }
    
    public bool CheckGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCastSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }

        return false;
    }

    void ResetCoyoteTimer()
    {
        _coyoteTimer.Pause();
        _coyoteTimer.Reset();
    }
    
    // Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, groundCastSize);
    }
}
