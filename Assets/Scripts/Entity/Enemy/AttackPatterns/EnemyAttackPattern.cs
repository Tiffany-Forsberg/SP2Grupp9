using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class EnemyAttackPattern
{
    public virtual void Setup() { }
    public abstract void Execute(EntityStats stats, LayerMask hostileLayer, List<AbilityExecutor> executors, AttackBehaviour attackPrefab);
}
