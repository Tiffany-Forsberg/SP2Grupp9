using UnityEngine;
using UnityEngine.Events;
using Vector2 = System.Numerics.Vector2;

[CreateAssetMenu(fileName = "DamageEventPort", menuName = "EventPorts/DamageEventPort", order = 0)]
public class EntityEffectEventPort : ScriptableObject
{
    // Target, Amount
    public event UnityAction<GameObject, int> DamageEvent;
    // Target, Amount
    public event UnityAction<GameObject, int> HealingEvent;
    // Caster, Target, Power
    public event UnityAction<GameObject, GameObject, float> KnockbackEvent;
    public event UnityAction<GameObject, GameObject, float> SelfPushbackEvent;

    public void DealDamage(GameObject damageable, int damage)
    {
        DamageEvent?.Invoke(damageable, damage);
    }
    
    public void Heal(GameObject damageable, int healing)
    {
        HealingEvent?.Invoke(damageable, healing);
    }

    public void ApplyKnockback(GameObject caster, GameObject target, float power)
    {
        KnockbackEvent?.Invoke(caster, target, power);
    }

    public void ApplySelfPushback(GameObject caster, GameObject target, float power)
    {
        SelfPushbackEvent?.Invoke(caster, target, power);
    }
}