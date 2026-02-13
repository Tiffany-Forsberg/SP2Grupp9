using System;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private EntityStats entityStats;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private LayerMask hostileLayer;
    
    [Header("Attack 1")]
    [SerializeField] private List<AbilityExecutor> executors;
    [SerializeField] private AttackBehaviour attackPrefab;
    [SerializeField] private float baseTimeBetweenAttacks;
    [SerializeField] private float baseAttackSpeed;
    
    private float AttackSpeed =>
        (baseAttackSpeed - entityStats.AttackSpeedFlatIncrease) * entityStats.AttackSpeedMultiplier;

    private float TimeBetweenAttacks => Math.Max(baseTimeBetweenAttacks - AttackSpeed, 0);
    
    private CountdownTimer _attack1Cooldown;

    private Vector2 _direction = Vector2.right;
    private Vector2 _lastHorizontalDirection = Vector2.right;
    private Vector2 _lastHeldDirection = Vector2.right;
    private bool _downwardsHeld = false;

    [SerializeField] private UnityEvent onAttack1; 

    private void Start()
    {
        _attack1Cooldown = new CountdownTimer(TimeBetweenAttacks);
    }

    private void Update()
    {
        if (_direction == Vector2.down && groundCheck.CheckGrounded()) _direction = _lastHorizontalDirection;
        //if (_downwardsHeld && !groundCheck.CheckGrounded()) _direction = Vector2.down;
        UpdateAttackDirection();
    }

    public void GetAttackDirection(InputAction.CallbackContext context)
    {
        _lastHeldDirection = context.ReadValue<Vector2>();
    }

    private void UpdateAttackDirection()
    {
        _downwardsHeld = false;
        
        if (_lastHeldDirection.y > 0)
        {
            _direction = Vector2.up;
        }
        else if (_lastHeldDirection.y < 0 && !groundCheck.CheckGrounded())
        {
            _direction = Vector2.down;
            _downwardsHeld = true;
        }
        else if (_lastHeldDirection.x > 0)
        {
            _direction = Vector2.right;
            _lastHorizontalDirection = Vector2.right;
        }
        else if (_lastHeldDirection.x < 0)
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
            AttackBehaviour attackBehaviour = Instantiate(attackPrefab, transform.position + (Vector3)_direction*2, transform.rotation, transform);
            attackBehaviour.Setup(executors, hostileLayer, _direction);
            attackBehaviour.transform.parent = transform;
            _attack1Cooldown.Reset(TimeBetweenAttacks);
            _attack1Cooldown.Start();
            onAttack1?.Invoke();
        }
    }
}
