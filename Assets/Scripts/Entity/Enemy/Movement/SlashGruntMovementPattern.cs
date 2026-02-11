using System;
using UnityEngine;

[Serializable]
public class SlashGruntMovementPattern : EnemyMovementPattern
{
    [SerializeReference] public DirectionChangeOnImpactPattern directionChangeOnImpactPattern;
    [Tooltip("The base movement used when not in aggro")]
    [SerializeReference] private StepMovementPattern normalMovement;
    [Header("Aggro settings")]
    [Tooltip("The movement used when in aggro")]
    [SerializeReference] private StepMovementPattern aggroMovement;
    [Tooltip("The pattern used to FindAggro")]
    [SerializeReference] private FindAggro aggroPattern;

    [SerializeField] private Vector2 direction;

    public override void Setup()
    {
        normalMovement.MovingRight = direction.x > 0;
        directionChangeOnImpactPattern.Direction = direction;
    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        if (!aggroPattern.HasAggro)
        {
            normalMovement.Execute(movement, hostileLayers, groundCheck);
            directionChangeOnImpactPattern.Execute(movement, hostileLayers, groundCheck);
            
            normalMovement.MovingRight = directionChangeOnImpactPattern.Direction.x > 0;
            aggroPattern.Execute(movement, hostileLayers, groundCheck);
        }
    }
}
