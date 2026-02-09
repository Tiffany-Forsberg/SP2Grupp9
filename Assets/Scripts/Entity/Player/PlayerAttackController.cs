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
    private Vector2 _lastHorizontalDirection = Vector2.right;
    private bool _downwardsHeld = false;

    private void Start()
    {
        _attack1Cooldown = new CountdownTimer(Attack1Cooldown);
    }

    private void Update()
    {
        if (_direction == Vector2.down && groundCheck.CheckGrounded()) _direction = _lastHorizontalDirection;
        if (_downwardsHeld && !groundCheck.CheckGrounded()) _direction = Vector2.down;
    }

    public void GetAttackDirection(InputAction.CallbackContext context)
    {
        Vector2 newDirection = context.ReadValue<Vector2>();
        _downwardsHeld = false;

        if (newDirection.y > 0)
        {
            _direction = Vector2.up;
        }
        else if (newDirection.y < 0 && !groundCheck.CheckGrounded())
        {
            _direction = Vector2.down;
            _downwardsHeld = true;
        }
        else if (newDirection.x > 0)
        {
            _direction = Vector2.right;
            _lastHorizontalDirection = Vector2.right;
        }
        else if (newDirection.x < 0)
        {
            _direction = Vector2.left;
            _lastHorizontalDirection = Vector2.left;
        }
        else
        {
            _direction = _lastHorizontalDirection;
        }
    }
    
    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.started && !_attack1Cooldown.IsRunning)
        {
            AttackBehaviour attackBehaviour = Instantiate(attackPrefab, transform.position, transform.rotation);
            attackBehaviour.Setup(executors, hostileLayer, _direction);
            _attack1Cooldown.Reset();
        }
    }
    
}
