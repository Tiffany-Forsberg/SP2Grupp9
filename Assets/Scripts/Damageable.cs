using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [Tooltip("This entity's EntityStats component")]
    [SerializeField] private EntityStats stats;
    [Tooltip("The global DamageEventPort")]
    [SerializeField] private HealthChangeEventPort healthChangeEventPort;
    
    [Tooltip("The value used as the base max health for the entity")]
    [SerializeField] private int baseMaxHealth;

    private int MaxHealth => (int) ((baseMaxHealth + stats.HealthFlatIncrease) * stats.HealthMultiplier);
    private int _currentHealth;
    
    [Tooltip("The value used as the base max health for the entity")]
    [SerializeField] private int baseDefense;

    private int Defense => (int) ((baseDefense + stats.DefenseFlatIncrease) * stats.DefenseMultiplier);

    [Tooltip("Triggers when receiving healing")]
    [SerializeField] private UnityEvent onReceiveHeal;
    [Tooltip("Triggers when taking non zero damage")]
    [SerializeField] private UnityEvent onTakeDamage;
    [Tooltip("Triggers when taking zero damage due to it being fully blocked")]
    [SerializeField] private UnityEvent onDamageFullyBlocked;
    [Tooltip("Triggers when entity dies")]
    [SerializeField] private UnityEvent onDeath;
    
    private void Awake()
    {
        _currentHealth = MaxHealth;
    }

    private void OnEnable()
    {
        healthChangeEventPort.DamageEvent += ReceiveDamage;
        healthChangeEventPort.HealingEvent += ReceiveHealing;
    }

    private void OnDisable()
    {
        healthChangeEventPort.DamageEvent -= ReceiveDamage;
        healthChangeEventPort.HealingEvent -= ReceiveHealing;
    }

    private void ReceiveDamage(Damageable target, int damage)
    {
        if (this != target) return;
        int damageTaken = Math.Max(damage - Defense, 0);
        _currentHealth = Math.Max(_currentHealth - damageTaken, 0);

        if (damageTaken <= 0)
        {
            onDamageFullyBlocked?.Invoke();
        }
        else
        {
            onTakeDamage?.Invoke();
        }
        

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void ReceiveHealing(Damageable target, int healing)
    {
        if (this != target) return;
        _currentHealth = Math.Min(_currentHealth + healing, MaxHealth);
    }

    private void Die()
    {
        onDeath?.Invoke();
    }
}
