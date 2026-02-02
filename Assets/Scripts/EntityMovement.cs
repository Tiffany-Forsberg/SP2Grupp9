using System;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private EntityStats stats;
    [SerializeField] private Rigidbody2D rigidbody2D;
    
    [field: Header("Horizontal/Grounded Movement")]
    
    [Tooltip("Base max speed before modifiers are applied")]
    [SerializeField] private float baseMaxSpeed;
    private float MaxSpeed => (baseMaxSpeed + stats.FlatSpeedIncrease) * stats.SpeedMultiplier; 

    [Tooltip("Base acceleration before modifiers are applied")]
    [SerializeField] private float baseAcceleration;
    private float Acceleration => (baseAcceleration + stats.FlatAccelerationIncrease) * stats.AccelerationMultiplier;
    [Tooltip("Base deceleration before modifiers are applied")]
    [SerializeField] private float baseDeceleration;
    private float Deceleration => (baseDeceleration + stats.FlatDecelerationIncrease) * stats.AccelerationMultiplier;

    [field: Header("Vertical/Air Movement")] 
    
    [Tooltip("Spontaneous force applied when jumping")]
    [SerializeField] private float jumpForce;
    [Tooltip("The height achieved when the jump is fully held")]
    [SerializeField] private float maxJumpHeight;
    [Tooltip("The time required to reach the max jump height while holding")]
    [SerializeField] private float timeToReachMaxHeight;
    [Tooltip("The downwards force applied when in the air")]
    [SerializeField] private float gravity;
    
    [Tooltip("Base acceleration in the air before modifiers are applied. If 0, uses base acceleration instead")] 
    [SerializeField] private float baseAirAcceleration;
    private float AirAcceleration => (baseAirAcceleration + stats.FlatAccelerationIncrease) * stats.AccelerationMultiplier;
    [Tooltip("Base deceleration in the air before modifiers are applied. If 0, uses base acceleration instead")]
    [SerializeField] private float baseAirDeceleration;
    private float AirDeceleration => (baseAirDeceleration + stats.FlatAccelerationIncrease) * stats.AccelerationMultiplier;

    private void OnValidate()
    {
        if (rigidbody2D == null)
        {
            Debug.LogError($"Rigidbody2D is not assigned in {name}", this);
        }

        if (stats == null)
        {
            Debug.LogError($"Stats is not assigned in {name}", this);
        }
    }

    public void HandleHorizontalMovement(int direction)
    {
        int signOfDirection = Math.Sign(direction);

        float newVelocityX;
        
        int currentSignOfDirection = Math.Sign(rigidbody2D.linearVelocityX);
        bool changingDirection = signOfDirection != currentSignOfDirection && currentSignOfDirection != 0;
        
        if (signOfDirection != 0)
        {
            float acceleration = changingDirection ? Acceleration + Deceleration : Acceleration;
            newVelocityX = rigidbody2D.linearVelocityX + acceleration * Time.fixedDeltaTime * signOfDirection;
        }
        else
        {
            newVelocityX = Mathf.MoveTowards(rigidbody2D.linearVelocityX, 0, Time.fixedDeltaTime * Deceleration);
        }
        
        rigidbody2D.linearVelocityX = Mathf.Clamp(newVelocityX, -MaxSpeed, MaxSpeed);
    }

    public void Jump()
    {
        
    }
}
