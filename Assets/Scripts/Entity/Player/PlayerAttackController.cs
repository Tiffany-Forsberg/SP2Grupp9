using System;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private EntityStats entityStats;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private LayerMask hostileLayer;
    
    [Header("Attack 1")]
    
    [SerializeField] private List<AbilityExecutor> executors;

    [SerializeField] private AttackBehaviour attackPrefab;

    [SerializeField] private float baseAttack1Cooldown;

    private float Attack1Cooldown =>
        (baseAttack1Cooldown - entityStats.CooldownFlatDecrease) * entityStats.CooldownMultiplier;

    private CountdownTimer _attack1Cooldown;

    private Vector2 _direction = Vector2.right;

    private void Start()
    {
        _attack1Cooldown = new CountdownTimer(Attack1Cooldown);
    }
    
    public void GetAttackDirection(InputAction.CallbackContext context)
    {
        Vector2 newDirection = context.ReadValue<Vector2>();

        if (newDirection.y > 0)
        {
            _direction = Vector2.up;
        }
        else if (newDirection.y < 0 && !groundCheck.CheckGrounded())
        {
            _direction = Vector2.down;
        }
        else if (newDirection.x > 0)
        {
            _direction = Vector2.right;
        }
        else if (newDirection.x < 0)
        {
            _direction = Vector2.left;
        }
    }
    
    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.started && !_attack1Cooldown.IsRunning)
        {
            AttackBehaviour attackBehaviour = Instantiate(attackPrefab);
            attackBehaviour.Setup(executors, hostileLayer, _direction);
            _attack1Cooldown.Reset();
        }
    }
    
}
