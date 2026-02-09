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

    public void Setup(List<AbilityExecutor> executors, LayerMask hostileLayers)
    {
        _executors = executors;
        _hostileLayers = hostileLayers;
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
