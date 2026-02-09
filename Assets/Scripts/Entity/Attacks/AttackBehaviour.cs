using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackBehaviour : MonoBehaviour
{
    // TODO: HIDE
    public List<AbilityExecutor> _executors;
    public LayerMask _hostileLayers;
    
    public UnityEvent OnHit;
    public UnityEvent OnCollision;
    public Vector2 AttackDirection;

    public void Setup(List<AbilityExecutor> executors, LayerMask hostileLayers, Vector2 attackDirection)
    {
        _executors = executors;
        _hostileLayers = hostileLayers;
        AttackDirection = attackDirection;
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
