using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class BulletPattern : AttackPatterns
{
    [SerializeField] private GameObject self;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private float bulletSpeed;

    public override void Execute(Vector2 direction)
    {
        rigidbody2D.linearVelocity = direction.normalized * bulletSpeed;
    }
}
