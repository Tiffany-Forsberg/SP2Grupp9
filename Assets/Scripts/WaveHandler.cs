using System;
using UnityEngine;
using UnityEngine.Events;

public class WaveHandler : MonoBehaviour
{
    [Tooltip("The event port scriptable object that is activated on enemy spawned")]
    [SerializeField] private EventPort onEnemySpawned;
    [Tooltip("The event port scriptable object that is activated on enemy death")]
    [SerializeField] private EventPort onEnemyDeath;
    [Tooltip("The event port scriptable object that is activated when wave is started")]
    [SerializeField] private UnityEvent onWaveStarted;
    [Tooltip("The event port scriptable object that is activated when wave is over")]
    [SerializeField] private UnityEvent onWaveFinished;
    
    private int _currentEnemyCount = 0;

    private void OnEnable()
    {
        onEnemySpawned.OnEventRaised += EnemySpawned;
        onEnemyDeath.OnEventRaised += EnemyDied;
    }

    private void OnDisable()
    {
        onEnemySpawned.OnEventRaised -= EnemySpawned;
        onEnemyDeath.OnEventRaised -= EnemyDied;
    }

    private void EnemySpawned()
    {
        if (_currentEnemyCount == 0)
        {
            onWaveStarted?.Invoke();
        }

        _currentEnemyCount += 1;
    }

    private void EnemyDied()
    {
        _currentEnemyCount -= 1;
        
        if (_currentEnemyCount == 0)
        {
            onWaveFinished?.Invoke();
        }
    }
}
