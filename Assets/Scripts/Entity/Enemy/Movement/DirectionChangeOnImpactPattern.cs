using System;
using UnityEngine;

[Serializable]
public class DirectionChangeOnImpactPattern : EnemyMovementPattern
{
    [SerializeField] public Rigidbody2D rigidbody2D;
    
    [Header("Raycast settings")]
    [Tooltip("The length of the raycast")]
    [SerializeField] public float raycastDistance;
    [Tooltip("The layers that should be checked for impact")]
    [SerializeField] private LayerMask impactLayers;
    
    [Tooltip("Is used to determine whether vertical impacts should be taken into account")]
    [SerializeField] private bool shouldUseVerticalImpacts = false;
    [Tooltip("Is used to determine whether horizontal impacts should be taken into account")]
    [SerializeField] private bool shouldUseHorizontalImpacts = false;
    
    [Tooltip("Will change on impact but should be assigned at start. (Note: this may be set at start by other scripts if part of a larger pattern)")]
    public Vector2 Direction;

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        HandleImpact();
    }

    private void HandleImpact()
    {
        RaycastHit2D hit;
        
        if (shouldUseHorizontalImpacts && rigidbody2D.linearVelocityX == 0)
        {
            hit = Physics2D.Raycast(rigidbody2D.position, Vector2.right * Math.Sign(Direction.x), raycastDistance, impactLayers);
            if (hit)
            {
                Debug.Log($"Impact hit, normal: {hit.normal}");
                Direction = Vector2.Reflect(Direction, hit.normal);
                return;
            }
        }
        
        if (shouldUseVerticalImpacts && rigidbody2D.linearVelocityY == 0)
        {
            hit = Physics2D.Raycast(rigidbody2D.position, Vector2.up * Math.Sign(Direction.y), raycastDistance, impactLayers);
            if (hit)
            {
                Direction = Vector2.Reflect(Direction, hit.normal);
                return;
            }
        }
    }
}
