
using System.Collections.Generic;
using UnityEngine;

public class AttackOnPositionReachedPattern : EnemyAttackPattern
{
    [SerializeField] private EntityEvent entityEventHandler;
    [SerializeField] private BoxCollider2D collider;
    private Vector2 _targetPosition;
    

    public override void Setup()
    {
        entityEventHandler.OnVectorEvent += GetTarget;
    }

    public override void Execute(EntityStats stats, LayerMask hostileLayer, List<AbilityExecutor> executors,
        AttackBehaviour attackPrefab)
    {
        if (collider.OverlapPoint(_targetPosition)) entityEventHandler.InvokeOnEvent();
    }

    private void GetTarget(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
