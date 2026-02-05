using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    // TODO: Add tooltips
    
    [field: Header("Damage and Attacks")]
    [Tooltip("Multiplier for damage dealt")]
    [Min(0f)] public float DamageMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base damage dealt")]
    public int DamageFlatIncrease = 0;
    [Tooltip("Multiplier for knockback dealt")]
    [Min(0f)] public float KnockbackDealtMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base knockback dealt")]
    public float KnockbackDealtFlatIncrease = 0;
    [Tooltip("Multiplier for attack speed of basic attack")]
    [Min(0f)] public float AttackSpeedMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base attack speed")]
    public float AttackSpeedFlatIncrease = 0;

    [field: Header("Health and durability")]
    [Tooltip("Multiplier for max health")]
    [Min(0f)] public float HealthMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base max health")]
    public int HealthFlatIncrease = 0; 
    [Tooltip("Multiplier for the defense of this entity")]
    [Min(0f)] public float DefenseMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base defense")]
    public int DefenseFlatIncrease = 0;
    [Tooltip("Multiplier for knockback reduction")]
    [Min(0f)] public float KnockbackReductionMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base knockback reduction. This much knockback will be deducted")]
    public float KnockbackReductionFlatIncrease = 0f;

    [field: Header("Speed and Movement")]
    [Tooltip("Multiplier for max speed")]
    [Min(0f)] public float SpeedMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base max speed")]
    public float SpeedFlatIncrease = 0;
    [Tooltip("Multiplier for acceleration")]
    [Min(0f)] public float AccelerationMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base acceleration")]
    public float AccelerationFlatIncrease = 0;
    [Tooltip("Multiplier for deceleration")]
    [Min(0f)] public float DecelerationMultiplier = 1.0f;
    [Tooltip("Flat increase added onto base deceleration")]
    public float DecelerationFlatIncrease = 0;
    
    [field: Header("Miscellaneous")]
    [Tooltip("Multiplier for all cooldowns (NOTE: lower multiplier means shorter cooldown)")]
    [Min(0f)] public float CooldownMultiplier = 1.0f;
    // NOTE: THIS IS A DECREASE, UNLIKE MOST FLAT VARIABLES HERE
    [Tooltip("Flat decrease added onto all cooldowns (NOTE: higher values equal shorter cooldowns)")]
    public float CooldownFlatDecrease = 1.0f;
}
