using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private EventPort onDeath;
    
    public void Die()
    {
        onDeath.RaiseEvent();
        Destroy(gameObject);
    }
}
