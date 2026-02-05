using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DamageEventPort", menuName = "EventPorts/DamageEventPort", order = 0)]
public class HealthChangeEventPort : ScriptableObject
{
    public event UnityAction<Damageable, int> DamageEvent;
    public event UnityAction<Damageable, int> HealingEvent;

    public void DealDamage(Damageable damageable, int damage)
    {
        DamageEvent?.Invoke(damageable, damage);
    }
    
    public void Heal(Damageable damageable, int healing)
    {
        HealingEvent?.Invoke(damageable, healing);
    }
}