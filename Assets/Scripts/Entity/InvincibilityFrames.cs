using System;
using ImprovedTimers;
using UnityEngine;
using UnityEngine.Events;

public class InvincibilityFrames : MonoBehaviour
{
    [Tooltip("This entities Damageable component")]
    [SerializeField] private Damageable damageable;
    
    [Tooltip("The duration of invincibility")]
    [SerializeField] private float duration;
    private CountdownTimer _invincibilityTimer;
    
    [SerializeField] private UnityEvent onInvincibilityStarted;
    [SerializeField] private UnityEvent onInvincibilityFinished;
    
    public bool Invincible => _invincibilityTimer.IsRunning;
    
    private void OnEnable()
    {
        _invincibilityTimer = new CountdownTimer(duration);
        _invincibilityTimer.OnTimerStart += HandleInvincibilityStarted;
        _invincibilityTimer.OnTimerStop += HandleInvincibilityFinished;
        damageable.OnTakeDamage.AddListener(StartInvincibility);
    }

    private void OnDisable()
    {
        _invincibilityTimer.OnTimerStart -= HandleInvincibilityStarted;
        _invincibilityTimer.OnTimerStop -= HandleInvincibilityFinished;
        damageable.OnTakeDamage.RemoveListener(StartInvincibility);
    }

    private void StartInvincibility()
    {
        _invincibilityTimer.Start();
    }

    private void HandleInvincibilityStarted()
    {
        onInvincibilityStarted?.Invoke();
    }
    
    private void HandleInvincibilityFinished()
    {
        onInvincibilityFinished?.Invoke();
    }
}
