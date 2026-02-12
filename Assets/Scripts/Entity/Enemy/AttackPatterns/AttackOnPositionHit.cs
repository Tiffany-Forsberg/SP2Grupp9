
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackOnPositionHit : EnemyAttackPattern
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
        if (collider.OverlapPoint(_targetPosition))
        {
            AttackBehaviour attackBehaviour = UnityEngine.Object.Instantiate(attackPrefab, collider.transform.position, collider.transform.rotation);
            attackBehaviour.Setup(executors, hostileLayer, Vector2.zero);
            entityEventHandler.InvokeOnEvent();
        }
    }

    private void GetTarget(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
