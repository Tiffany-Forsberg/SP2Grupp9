using System;
using UnityEngine;
using UnityEngine.Events;

public class Knockbackable : MonoBehaviour
{
    [Tooltip("This entity's EntityStats component")]
    [SerializeField] private EntityStats stats;
    [Tooltip("The global DamageEventPort")]
    [SerializeField] private EntityEffectEventPort entityEffectEventPort;
    [SerializeField] private Rigidbody2D rigidbody2D;
    
    
    [Tooltip("The base value used for knockback reduction")]
    [SerializeField] private float baseKnockbackReduction;

    private float KnockbackReduction => (baseKnockbackReduction + stats.KnockbackReductionFlatIncrease) *
                                        stats.KnockbackReductionMultiplier;
    
    [Tooltip("Triggers when taking knockback")]
    [SerializeField] private UnityEvent onKnockback;
    
    private void OnEnable()
    {
        entityEffectEventPort.KnockbackEvent += ReceiveKnockback;
        entityEffectEventPort.SelfPushbackEvent += ReceiveSelfPushback;
    }

    private void OnDisable()
    {
        entityEffectEventPort.KnockbackEvent -= ReceiveKnockback;
        entityEffectEventPort.SelfPushbackEvent -= ReceiveSelfPushback;
    }

    private void ReceiveKnockback(GameObject caster, GameObject target, float power)
    {
        if (gameObject != target) return;

        float knockback = Math.Max(power - KnockbackReduction, 0);
        
        Vector2 direction = (target.transform.position - caster.transform.position).normalized;
        
        rigidbody2D.AddForce(direction * knockback, ForceMode2D.Impulse);
    }

    private void ReceiveSelfPushback(GameObject caster, GameObject target, float power)
    {
        if (gameObject != caster) return;
        
        Vector2 direction = (target.transform.position - caster.transform.position).normalized;
        rigidbody2D.AddForce(direction * -power, ForceMode2D.Impulse);
    }
}
