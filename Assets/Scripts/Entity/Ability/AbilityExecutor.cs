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

    [SerializeField] private AnimatorController animatorController;
    private CountdownTimer _castTimer;

    void Awake()
    {
        _castTimer = new CountdownTimer(ability.CastTime);
        // _castTimer.OnTimerStart = () => animatorController.OrNull()?.PlayOneShot(ability.animationClip);
    }

    public void Execute(GameObject target)
    {
        if (!target) return;
        foreach (var effect in ability.Effects)
        {
            effect.Execute(gameObject, target, stats);
        }
    }
}
