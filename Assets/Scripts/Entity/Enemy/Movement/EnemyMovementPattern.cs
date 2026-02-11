using System;
using UnityEngine;

[Serializable]
public abstract class EnemyMovementPattern
{
    public abstract void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck);
}
