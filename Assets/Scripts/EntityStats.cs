using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [field: Header("Damage and Attacks")]
    [field: SerializeField] public float DamageMultiplier { get; private set; } = 1.0f;
    [field: SerializeField] public int FlatDamageIncrease { get; private set; } = 0;
    [field: SerializeField] public float KnockbackMultiplier { get; private set; } = 1.0f;
    [field: SerializeField] public float AttackSpeedMultiplier { get; private set; } = 1.0f;

    [field: Header("Health")]
    [field: SerializeField] public float HealthMultiplier { get; private set; } = 1.0f;
    [field: SerializeField] public int FlatHealthIncrease { get; private set; } = 0;    

    [field: Header("Speed and Movement")]
    [field: SerializeField] public float SpeedMultiplier { get; private set; } = 1.0f;
    [field: SerializeField] public float FlatSpeedIncrease { get; private set; } = 0;
    [field: SerializeField] public float AccelerationMultiplier { get; private set; } = 1.0f;
    [field: SerializeField] public float DecelerationMultiplier { get; private set; } = 1.0f;
    
    [field: Header("Miscellaneous")]
    [field: SerializeField] [Min(0)] public float CooldownMultiplier { get; private set; } = 1.0f;
}
