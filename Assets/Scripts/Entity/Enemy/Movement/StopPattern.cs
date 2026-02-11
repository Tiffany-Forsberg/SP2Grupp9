
using UnityEngine;

public class StopPattern : EnemyMovementPattern
{
    [SerializeField] private EntityMovement movement;

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        movement.Stop();
    }
}
