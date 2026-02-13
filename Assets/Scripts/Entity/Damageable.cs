using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [Tooltip("This entity's EntityStats component")]
    [SerializeField] private EntityStats stats;
    [Tooltip("The global DamageEventPort")]
    [SerializeField] private EntityEffectEventPort entityEffectEventPort;
    
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
    public UnityEvent OnTakeDamage;
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
        entityEffectEventPort.DamageEvent += ReceiveDamage;
        entityEffectEventPort.HealingEvent += ReceiveHealing;
    }

    private void OnDisable()
    {
        entityEffectEventPort.DamageEvent -= ReceiveDamage;
        entityEffectEventPort.HealingEvent -= ReceiveHealing;
    }

    private void ReceiveDamage(GameObject target, int damage)
    {
        if (gameObject != target) return;
        int damageTaken = Math.Max(damage - Defense, 0);
        _currentHealth = Math.Max(_currentHealth - damageTaken, 0);

        if (damageTaken <= 0)
        {
            onDamageFullyBlocked?.Invoke();
        }
        else
        {
            OnTakeDamage?.Invoke();
        }
        

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void ReceiveHealing(GameObject target, int healing)
    {
        if (gameObject != target) return;
        _currentHealth = Math.Min(_currentHealth + healing, MaxHealth);
    }

    private void Die()
    {
        onDeath?.Invoke();
    }
}
