using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class StepMovementPattern : EnemyMovementPattern
{
    [Min(0.0f)] [SerializeField] private float _tinyStepSpeed;
    [Min(0.0f)] [SerializeField] private float _tinyStepDuration;
    [Min(0.0f)] [SerializeField] private float _bigStepSpeed;
    [Min(0.0f)] [SerializeField] private float _bigStepDuration;

    [SerializeField] private Collider2D collider;
    
    [Tooltip("Handles the characters movement direction")]
    public bool MovingRight = true;
    
    private int Direction => MovingRight ? 1 : -1;
    
    private bool _isSlowStepping;
    private float _timer;

    [HideInInspector] public bool ShouldUseTargetPosition;
    
    private Vector2 _targetPosition;
    
    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        _timer -= Time.fixedDeltaTime;
        if (_timer <= 0)
        {
            HandleStartStepLogic(movement);
        }
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void HandleStartStepLogic(EntityMovement movement)
    {
        _isSlowStepping = !_isSlowStepping;
        if (_isSlowStepping)
        {
            movement.HandleStep(_tinyStepSpeed * Direction, () => _isSlowStepping && !HandleOverlap());
            _timer = _tinyStepDuration;
        }
        else
        {
            movement.HandleStep(_bigStepSpeed * Direction, () => !_isSlowStepping && !HandleOverlap());
            _timer = _bigStepDuration;
        }
    }

    private bool HandleOverlap()
    {
        if (!ShouldUseTargetPosition) return false;
        if (collider.OverlapPoint(_targetPosition)) return true;
        return false;
    }
}
