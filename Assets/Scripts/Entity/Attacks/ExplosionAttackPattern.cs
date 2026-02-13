using System;
using UnityEngine;

public class ExplosionAttackPattern : AttackPatterns
{
    [SerializeField] private GameObject self;
    
    [SerializeField] [Min(0)] private float timeToReachTargetSize;
    [SerializeField] [Min(0)] private float holdTime;
    [SerializeField] [Min(0)] private float targetSize;
    
    private float _timeElapsed;
    private float _currentSize;

    public override void Execute(Vector2 direction)
    {
        _timeElapsed += Time.fixedDeltaTime;
        
        _currentSize = Mathf.Lerp(0, targetSize, _timeElapsed / timeToReachTargetSize);
        self.transform.localScale = new Vector3(_currentSize, _currentSize, _currentSize);

        if (_timeElapsed >= timeToReachTargetSize + holdTime)
        {
            DestroySelf();
        }
    }
    
    private void DestroySelf()
    {
        UnityEngine.Object.DestroyImmediate(self);
    }
}
