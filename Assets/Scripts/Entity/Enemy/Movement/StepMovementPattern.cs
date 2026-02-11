using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class StepMovementPattern : EnemyMovementPattern
{
    [SerializeField] private float _tinyStepSpeed;
    [SerializeField] private float _tinyStepDuration;
    [SerializeField] private float _bigStepSpeed;
    [SerializeField] private float _bigStepDuration;
    
    private bool _isSlowStepping;
    private float _timer;
    
    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        _timer -= Time.fixedDeltaTime;
        if (_timer <= 0)
        {
            HandleStartStepLogic(movement);
        }
    }

    private void HandleStartStepLogic(EntityMovement movement)
    {
        _isSlowStepping = !_isSlowStepping;
        if (_isSlowStepping)
        {
            movement.HandleStep(_tinyStepSpeed, () => _isSlowStepping);
            _timer = _tinyStepDuration;
        }
        else
        {
            movement.HandleStep(_bigStepSpeed, () => !_isSlowStepping);
            _timer = _bigStepDuration;
        }
    }
}
