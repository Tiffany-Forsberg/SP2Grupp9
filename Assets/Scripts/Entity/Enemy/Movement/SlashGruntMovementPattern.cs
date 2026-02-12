using System;
using UnityEngine;

[Serializable]
public class SlashGruntMovementPattern : EnemyMovementPattern
{
    [SerializeField] private EntityEvent eventHandler;
    
    [SerializeReference] public DirectionChangeOnImpactPattern directionChangeOnImpactPattern;
    [Tooltip("The base movement used when not in aggro")]
    [SerializeReference] private StepMovementPattern normalMovement;
    [Header("Aggro settings")]
    [Tooltip("The movement used when in aggro")]
    [SerializeReference] private StepMovementPattern aggroMovement;
    [Tooltip("The pattern used to FindAggro")]
    [SerializeReference] private FindAggro aggroPattern;
    [Tooltip("The stop pattern")]
    [SerializeReference] private StopPattern stopPattern;
    [SerializeField] private bool changeDirectionAfterDefend = true;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private bool changeDirectionAfterAttack = true;

    private Vector2 _startDirection;
    private bool _hasAttacked;
    

    public override void Setup()
    {
        directionChangeOnImpactPattern.Setup();
        normalMovement.Setup();
        aggroMovement.Setup();
        aggroPattern.Setup();
        stopPattern.Setup();
        
        normalMovement.MovingRight = direction.x > 0;
        aggroMovement.MovingRight = direction.x > 0;
        directionChangeOnImpactPattern.Direction = direction;
        _startDirection = direction;
    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        HandleDirectionChange();
        
        directionChangeOnImpactPattern.Execute(movement, hostileLayers, groundCheck);

        if (direction == _startDirection) aggroPattern.FlipVision(true);
        else aggroPattern.FlipVision(false);

        if (!aggroPattern.HasAggro)
        {
            normalMovement.Execute(movement, hostileLayers, groundCheck);
            aggroPattern.Execute(movement, hostileLayers, groundCheck);
        }
        else
        {
            if ((direction.x > 0  && rigidbody2D.position.x < aggroPattern.AggroTarget.x) || (direction.x < 0 && rigidbody2D.position.x > aggroPattern.AggroTarget.x))
            {
                aggroMovement.Execute(movement, hostileLayers, groundCheck);
            }
            else if (!_hasAttacked)
            {
                stopPattern.Execute(movement, hostileLayers, groundCheck);
                
            }
            else
            {
                if (changeDirectionAfterAttack)
                {
                    direction.x *= -1;
                    normalMovement.MovingRight = direction.x > 0;
                    aggroMovement.MovingRight = direction.x > 0;
                    directionChangeOnImpactPattern.Direction = direction;
                }
                aggroPattern.HasAggro = false;
            }
        }
    }

    private void HandleDirectionChange()
    {
        direction = directionChangeOnImpactPattern.Direction;
        normalMovement.MovingRight = direction.x > 0;
        aggroMovement.MovingRight = direction.x > 0;
    }
}
