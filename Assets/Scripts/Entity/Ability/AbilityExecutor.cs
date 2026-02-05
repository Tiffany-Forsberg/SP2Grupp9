using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ImprovedTimers;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;

public class AbilityExecutor : MonoBehaviour
{
    [SerializeField] private EntityStats stats;
    
    [SerializeField] private AbilityData ability;
    [SerializeField] private GameObject target;

    [SerializeField] private AnimatorController animatorController;
    private CountdownTimer _castTimer;

    void Awake()
    {
        _castTimer = new CountdownTimer(ability.CastTime);
        // _castTimer.OnTimerStart = () => animatorController.OrNull()?.PlayOneShot(ability.animationClip);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Execute(target);
        }
    }

    public void Execute(GameObject target)
    {
        foreach (var effect in ability.Effects)
        {
            effect.Execute(gameObject, target, stats);
        }
    }

    public void HandleBasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Execute(target);
        }
    }
}
