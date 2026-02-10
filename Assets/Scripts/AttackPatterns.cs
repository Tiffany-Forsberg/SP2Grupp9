using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public abstract class AttackPatterns
{
    public abstract void Execute(Vector2 direction);

}

[Serializable]
public class TemporaryAttack : AttackPatterns
{
    [SerializeField] private GameObject self;
    [SerializeField] private float duration;

    public override void Execute(Vector2 direction)
    {
        HandleLifetime();
    }

    private void HandleLifetime()
    {
        duration -= Time.fixedDeltaTime;
        if (duration <= 0)
        {
            Object.DestroyImmediate(self);
        }
    }
}
