using System;
using UnityEngine;

public class EnemyIdentifier : MonoBehaviour
{
    [SerializeField] public EventPort onSpawn;

    private void Start()
    {
        onSpawn.RaiseEvent();
    }
}
