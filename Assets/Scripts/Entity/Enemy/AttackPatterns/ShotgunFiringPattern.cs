using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;
using UnityEngine.Serialization;

public class ShotgunFiringPattern : EnemyAttackPattern
{
    [SerializeField] private VectorEvent firingEvent;
    
    [Tooltip("The normalized angle from the player direction that the two extra bullets should be fired (0 = 0 degrees, 1 = pi/180 degrees)")]
    [SerializeField, Range(0f, 1f)] private float firingArch;

    [Tooltip("The randomization applied to the firing arch value, high values may result in the extra bullets being fired in very erratic directions")]
    [SerializeField, Range(0f, 1f)] private float archRandomization;

    /*[Tooltip("The fire rate of this enemy")]
    [SerializeField] private float fireRate;
    private CountdownTimer firingCooldown;*/

    private bool fire = false;
    private Vector2 firingDirection;

    public override void Setup()
    {
        firingEvent.OnVectorEvent += AllowFiring;
    }

    public override void Execute(EntityStats stats, LayerMask hostileLayer, List<AbilityExecutor> executors, AttackBehaviour attackPrefab)
    {
        if (fire)
        {
            fire = false;
            
            FireInDirection(firingDirection, stats, hostileLayer, executors, attackPrefab);
            
            /*if (firingCooldown is null)
            {
                firingCooldown = new CountdownTimer(fireRate);
                firingCooldown.Reset();
                firingCooldown.Start();
            }
            
            if (!firingCooldown.IsFinished)
            {
                return;
            }
            
            fire = false;
            
            
            
            firingCooldown.Reset();*/
        }
    }

    private void FireInDirection(Vector2 direction, EntityStats stats, LayerMask hostileLayer, List<AbilityExecutor> executors, AttackBehaviour attackPrefab)
    {
        AttackBehaviour bullet1 = UnityEngine.Object.Instantiate(attackPrefab, stats.transform.position, Quaternion.identity);
        attackPrefab.Setup(executors, hostileLayer, direction);

        Vector2 localX = direction.normalized;
        Vector2 localY = new Vector2(direction.y, -direction.x).normalized;

        float extraAngle1 = Random.Range(-archRandomization, archRandomization);
        Vector2 exBullet1Direction = localX * Mathf.Cos(firingArch + extraAngle1) + localY * Mathf.Sin(firingArch + extraAngle1);
        
        AttackBehaviour exBullet1 = UnityEngine.Object.Instantiate(attackPrefab, stats.transform.position, Quaternion.identity);
        exBullet1.Setup(executors, hostileLayer, exBullet1Direction);
        
        float extraAngle2 = Random.Range(-archRandomization, archRandomization);
        Vector2 exBullet2Direction = localX * Mathf.Cos(firingArch + extraAngle2) - localY * Mathf.Sin(firingArch + extraAngle2);
        
        AttackBehaviour exBullet2 = UnityEngine.Object.Instantiate(attackPrefab, stats.transform.position, Quaternion.identity);
        exBullet2.Setup(executors, hostileLayer, exBullet2Direction);
    }

    public void AllowFiring(Vector2 direction)
    {
        fire = true;
        firingDirection = direction;
    }
}
