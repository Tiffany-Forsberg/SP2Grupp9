using System;
using UnityEngine;

[Serializable]
public class SlashGruntMovementPattern : EnemyMovementPattern
{
    [SerializeReference] private StepMovementPattern stepMovementPattern;
    [SerializeReference] public DirectionChangeOnImpactPattern directionChangeOnImpactPattern;

    [SerializeField] private Vector2 direction;

    public override void Setup()
    {
        stepMovementPattern.MovingRight = direction.x > 0;
        directionChangeOnImpactPattern.Direction = direction;
    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        stepMovementPattern.Execute(movement, hostileLayers, groundCheck);
        directionChangeOnImpactPattern.Execute(movement, hostileLayers, groundCheck);
        
        stepMovementPattern.MovingRight = directionChangeOnImpactPattern.Direction.x > 0;
    }
}
