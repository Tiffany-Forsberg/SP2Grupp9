using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

public class ShotgunFiringPattern : EnemyAttackPattern
{
    [SerializeField] private float maxFiringDistance;
    
    [Tooltip("The normalized angle from the player direction that the two extra bullets should be fired (0 = 0 degrees, 1 = pi/180 degrees)")]
    [SerializeField, Range(0f, 1f)] private float firingArch;

    [Tooltip("The randomization applied to the firing arch value, high values may result in the extra bullets being fired in very erratic directions")]
    [SerializeField, Range(0f, 1f)] private float archRandomization;

    [Tooltip("The fire rate of this enemy")]
    [SerializeField] private float fireRate;
    private CountdownTimer firingCooldown;
    
    public override void Execute(EntityStats stats, LayerMask hostileLayer, List<AbilityExecutor> executors, AttackBehaviour attackPrefab)
    {
        if (firingCooldown is null)
        {
            firingCooldown = new CountdownTimer(fireRate);
            firingCooldown.Reset();
        }
        
        if (!firingCooldown.IsFinished)
        {
            return;
        }
        
        // Fire
        
        
        
        firingCooldown.Reset();
    }
}
