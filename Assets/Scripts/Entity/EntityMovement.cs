using System;
using System.Collections;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private EntityStats stats;
    [SerializeField] private Rigidbody2D rigidbody2D;
    
    [field: Header("Horizontal/Grounded Movement")]
    
    [Tooltip("Base max speed before modifiers are applied")]
    [SerializeField] private float baseMaxSpeed;
    private float MaxSpeed => (baseMaxSpeed + stats.SpeedFlatIncrease) * stats.SpeedMultiplier; 

    [Tooltip("Base acceleration before modifiers are applied")]
    [SerializeField] private float baseAcceleration;
    private float Acceleration => (baseAcceleration + stats.AccelerationFlatIncrease) * stats.AccelerationMultiplier;
    [Tooltip("Base deceleration before modifiers are applied")]
    [SerializeField] private float baseDeceleration;
    private float Deceleration => (baseDeceleration + stats.DecelerationFlatIncrease) * stats.DecelerationMultiplier;

    [field: Header("Vertical/Air Movement")] 
    
    [Tooltip("The height achieved when the jump is fully held")]
    [SerializeField] private float maxJumpHeight;
    [Tooltip("The time required to reach the max jump height while holding")]
    [SerializeField] private float timeToReachMaxHeight;
    [Tooltip("The minimum time the jump is held")]
    [SerializeField] private float minJumpHoldTime;
    [Tooltip("The additional downwards force added when releasing the jump early")]
    [SerializeField] private float earlyReleaseGravity;
    [Tooltip("The force applied to smooth out jump endings")]
    [SerializeField] private float jumpEndForce;
    
    private Coroutine _jumpCoroutine;
    
    [Tooltip("Base acceleration in the air before modifiers are applied. If 0, uses base acceleration instead")] 
    [SerializeField] private float baseAirAcceleration;
    private float AirAcceleration => (baseAirAcceleration + stats.AccelerationFlatIncrease) * stats.AccelerationMultiplier;
    [Tooltip("Base deceleration in the air before modifiers are applied. If 0, uses base acceleration instead")]
    [SerializeField] private float baseAirDeceleration;
    private float AirDeceleration => (baseAirDeceleration + stats.DecelerationFlatIncrease) * stats.DecelerationMultiplier;

    [Tooltip("The maximum downwards velocity")]
    [SerializeField] [Range(0.0f, 25.67f)] private float maxFallSpeed;

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

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocityY = Mathf.Max(rigidbody2D.linearVelocityY, -maxFallSpeed);
    }

    public void HandleHorizontalMovement(int direction, bool isGrounded = true)
    {
        float usedAcceleration = isGrounded ? Acceleration : AirAcceleration;
        float usedDeceleration = isGrounded ? Deceleration : AirDeceleration;
        
        int signOfDirection = Math.Sign(direction);

        float newVelocityX;
        
        int currentSignOfDirection = Math.Sign(rigidbody2D.linearVelocityX);
        bool changingDirection = signOfDirection != currentSignOfDirection && currentSignOfDirection != 0;
        
        if (signOfDirection != 0)
        {
            float acceleration = changingDirection ? usedAcceleration + usedDeceleration : usedAcceleration;
            newVelocityX = rigidbody2D.linearVelocityX + acceleration * Time.fixedDeltaTime * signOfDirection;
        }
        else
        {
            newVelocityX = Mathf.MoveTowards(rigidbody2D.linearVelocityX, 0, Time.fixedDeltaTime * usedDeceleration);
        }
        
        rigidbody2D.linearVelocityX = Mathf.Clamp(newVelocityX, -MaxSpeed, MaxSpeed);
    }

    public void Jump(Func<bool> isJumping)
    {
        if (_jumpCoroutine != null)
        {
            StopCoroutine(_jumpCoroutine);
        }
        _jumpCoroutine = StartCoroutine(JumpCoroutine(isJumping));
    }

    private IEnumerator JumpCoroutine(Func<bool> isJumping)
    {
        float jumpForce =  maxJumpHeight/timeToReachMaxHeight;
        float timeSpent = 0;
        bool earlyRelease = false;

        while (timeSpent < timeToReachMaxHeight)
        {
            yield return new WaitForFixedUpdate();
            rigidbody2D.linearVelocityY = jumpForce;
            timeSpent += Time.fixedDeltaTime;

            if (timeSpent > minJumpHoldTime && !isJumping.Invoke())
            {
                earlyRelease = true;
                break;
            }
        }
        
        rigidbody2D.linearVelocityY = earlyRelease ? jumpEndForce + earlyReleaseGravity : jumpEndForce;
    }
}
