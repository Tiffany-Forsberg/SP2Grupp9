using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackBehaviour : MonoBehaviour
{
    private List<AbilityExecutor> _executors;
    private LayerMask _hostileLayers;

    [SerializeReference] private List<AttackPatterns> patterns;
    
    public UnityEvent OnHit;
    public UnityEvent OnCollision;
    private Vector2 _attackDirection;

    private void FixedUpdate()
    {
        foreach (AttackPatterns pattern in patterns)
        {
            pattern.Execute(_attackDirection);
        }
    }
    
    public void Setup(List<AbilityExecutor> executors, LayerMask hostileLayers, Vector2 attackDirection)
    {
        _executors = executors;
        _hostileLayers = hostileLayers;
        _attackDirection = attackDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _hostileLayers) != 0)
        {
            foreach (AbilityExecutor executor in _executors)
            {
                executor.Execute(other.gameObject);
            }
            OnHit?.Invoke();
        }
        OnCollision?.Invoke();
    }
}
