using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
public class AbilityData : ScriptableObject
{
    public string Name;
    [SerializeReference] public List<AbilityEffect> Effects;

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
    }
}

[Serializable]
public class KnockbackEffect : AbilityEffect
{
    [Tooltip("The global EntityEffectEventPort")]
    [SerializeField] private EntityEffectEventPort entityEffectEventPort;
    [Tooltip("The force applied on knockback")]
    public float Force;
    public override void Execute(GameObject caster, GameObject target, EntityStats stats)
    {
        int knockback = (int) ((Force + stats.KnockbackDealtFlatIncrease) * stats.KnockbackDealtMultiplier);
        
        entityEffectEventPort.ApplyKnockback(caster, target, knockback);
    }
}

[Serializable]
public class AttackSelfPushback : AbilityEffect
{
    [Tooltip("The global EntityEffectEventPort")]
    [SerializeField] private EntityEffectEventPort entityEffectEventPort;
    [Tooltip("The force applied to self")]
    public float Force;
    
    public override void Execute(GameObject caster, GameObject target, EntityStats stats)
    {
        entityEffectEventPort.ApplySelfPushback(caster, target, Force);
        Debug.Log($"{caster.name} received pushback of {Force} from {target.name}");
    }
}
