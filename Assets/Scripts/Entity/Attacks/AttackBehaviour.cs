using UnityEngine;
using UnityEngine.Events;

public class AttackBehaviour : MonoBehaviour
{
    // TODO: HIDE
    public AbilityExecutor _executor;
    public LayerMask _hostileLayers;
    
    public UnityEvent OnHit;
    public UnityEvent OnCollision;

    public void Setup(AbilityExecutor executor, LayerMask hostileLayers)
    {
        _executor = executor;
        _hostileLayers = hostileLayers;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _hostileLayers) != 0)
        {
            _executor.Execute(other.gameObject);
            OnHit?.Invoke();
        }
        OnCollision?.Invoke();
    }
}
