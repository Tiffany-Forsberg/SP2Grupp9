using System;
using UnityEngine;

[Serializable]
public class SlashGruntMovementPattern : EnemyMovementPattern
{
    [SerializeField] private EntityEvent eventHandler;
    [SerializeField] private EntityEvent attackEvent;
    
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

    [SerializeField] private Vector2 direction;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Collider2D collider2D;

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
        
        aggroMovement.ShouldUseTargetPosition = true;

        eventHandler.OnEvent = () => _hasAttacked = true;
    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        directionChangeOnImpactPattern.Execute(movement, hostileLayers, groundCheck);
        HandleDirectionChange();
        
        bool movingRight = direction.x > 0;
        if (
            (movingRight && rigidbody2D.position.x > aggroPattern.AggroTarget.x) ||
            (!movingRight && rigidbody2D.position.x < aggroPattern.AggroTarget.x)
        )
        {
            aggroPattern.HasAggro = false;
        }

        if (direction == _startDirection) aggroPattern.FlipVision(true);
        else aggroPattern.FlipVision(false);

        if (!aggroPattern.HasAggro)
        {
            normalMovement.Execute(movement, hostileLayers, groundCheck);
            aggroPattern.Execute(movement, hostileLayers, groundCheck);
        }
        else
        {
            aggroMovement.SetTargetPosition(aggroPattern.AggroTarget);

            Debug.Log("Has a target");
            
            if (!collider2D.OverlapPoint(aggroPattern.AggroTarget))
            {
                aggroMovement.Execute(movement, hostileLayers, groundCheck);
            } 
            else if (!_hasAttacked)
            {
                
            }
            else
            {
                aggroPattern.HasAggro = false;
                _hasAttacked = false;
                if (changeDirectionAfterAttack)
                {
                    direction.x *= -1;
                    normalMovement.MovingRight = direction.x > 0;
                    aggroMovement.MovingRight = direction.x > 0;
                    directionChangeOnImpactPattern.Direction = direction;
                }
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
