using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : MonoBehaviour
{
    [SerializeField] private EntityStats entityStats;
    [SerializeField] private LayerMask hostileLayer;
    [SerializeField] private List<AbilityExecutor> executors;
    [SerializeField] private AttackBehaviour attackPrefab;

    [SerializeReference] private EnemyAttackPattern pattern;
    
    private void Awake()
    {
        pattern.Setup();
    }

    private void FixedUpdate()
    {
        pattern.Execute(entityStats, hostileLayer, executors, attackPrefab);
    }
}
