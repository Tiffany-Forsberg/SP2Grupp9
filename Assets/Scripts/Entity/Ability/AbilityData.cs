using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string Name;
    [SerializeReference] public List<AbilityEffect> Effects;

    public AnimationClip animationClip;
    public float CastTime = 0f;

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(Name))
        {
            Name = name;
        }

        if (Effects == null)
        {
            Effects = new List<AbilityEffect>();
        }
    }
}

[Serializable]
public abstract class AbilityEffect
{
    public abstract void Execute(GameObject caster, GameObject target, EntityStats stats);
}

[Serializable]
public class DamageEffect : AbilityEffect
{
    [Tooltip("The global EntityEffectEventPort")]
    [SerializeField] private EntityEffectEventPort entityEffectEventPort;
    [Tooltip("The amount of damage that will be dealt")]
    public int Amount;
    public override void Execute(GameObject caster, GameObject target, EntityStats stats)
    {
        int damage = (int) ((Amount + stats.DamageFlatIncrease) * stats.DamageMultiplier);
        entityEffectEventPort.DealDamage(target, damage);
        //target.GetComponent<Damageable>().ApplyDamage(Amount);
        Debug.Log(caster.name + " dealt " + Amount + " damage to " + target.name);
    }
}

[Serializable]
public class KnockbackEffect : AbilityEffect
{
    [Tooltip("The global EntityEffectEventPort")]
    [SerializeField] private EntityEffectEventPort entityEffectEventPort;
    [Tooltip("The amount of damage that will be dealt")]
    public float Force;
    public override void Execute(GameObject caster, GameObject target, EntityStats stats)
    {
        Vector2 direction = (target.transform.position - caster.transform.position).normalized;
        // target.GetComponent<Rigidbody2D>().AddForce(direction * Force, ForceMode2D.Impulse);
        int knockback = (int) ((Force + stats.KnockbackDealtFlatIncrease) * stats.KnockbackDealtMultiplier);
        
        entityEffectEventPort.ApplyKnockback(caster, target, knockback);
        Debug.Log(caster.name + " knocked " + target.name + " with a force of " + Force + " in the direction of " + direction);
    }
}
