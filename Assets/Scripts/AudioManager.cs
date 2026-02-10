using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

/*public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; };
    
    [Header("Ambiance Emitter")]
    [SerializeField] private StudioEventEmitter ambianceCombatEmitter;
    [SerializeField] private StudioEventEmitter ambianceRestingEmitter;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    
}
*/