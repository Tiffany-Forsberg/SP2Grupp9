using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class EnemyAttackPattern
{
    public abstract void Execute(EntityStats stats, LayerMask hostileLayer, List<AbilityExecutor> executors, AttackBehaviour attackPrefab);
}
