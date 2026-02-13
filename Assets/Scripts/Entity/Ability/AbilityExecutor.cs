using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ImprovedTimers;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;
using System.Collections.Generic;

public class AbilityExecutor : MonoBehaviour
{
    [SerializeField] private EntityStats stats;
    
    [SerializeField] private AbilityData ability;
    
    [SerializeField] private bool ignoresInvincibility;

    public void Execute(GameObject target)
    {
        if (!target) return;
        if (!ignoresInvincibility)
        {
            InvincibilityFrames iFrames = target.GetComponent<InvincibilityFrames>();
            if (iFrames)
            {
                if (iFrames.Invincible) return;
            }
        }
        foreach (var effect in ability.Effects)
        {
            effect.Execute(gameObject, target, stats);
        }
    }
}
