using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [field: Header("Damage and Attacks")]
    public float DamageMultiplier = 1.0f;
    public int FlatDamageIncrease = 0;
    public float KnockbackMultiplier = 1.0f;
    public float AttackSpeedMultiplier = 1.0f;

    [field: Header("Health")]
    public float HealthMultiplier = 1.0f;
    public int FlatHealthIncrease = 0;    

    [field: Header("Speed and Movement")]
    public float SpeedMultiplier = 1.0f;
    public float FlatSpeedIncrease = 0;
    public float AccelerationMultiplier = 1.0f;
    public float DecelerationMultiplier = 1.0f;
    
    [field: Header("Miscellaneous")]
    [Min(0)] public float CooldownMultiplier = 1.0f;
}
