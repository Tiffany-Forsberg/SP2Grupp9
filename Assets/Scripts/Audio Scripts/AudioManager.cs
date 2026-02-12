using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public enum Location
{
    Combat, 
    Resting
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [Header("Ambiance Emitter")]
    [SerializeField] private StudioEventEmitter ambianceCombatEmitter;
    [SerializeField] private StudioEventEmitter ambianceRestingEmitter;
    
    [Header("Music Emitter")]
    [SerializeField] private StudioEventEmitter MusicEmitter;

    private StudioEventEmitter emitter;
    private void Awake()
    //Method to check that there is only ONE AudioManager in the current active scene
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

    private void Update()
    {
        //Method that tells the AudioManager to follow the game object with the tag "Player"
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void GetLocation(Location location)
    {
        //Switch method to determine what Ambiance to play
        switch (location)
        {
            case Location.Combat:
                emitter = ambianceCombatEmitter;
				emitter = MusicEmitter;
                break;
            case Location.Resting:
                emitter = ambianceRestingEmitter;
				emitter = MusicEmitter;
                break;
        }
    }

    public void PlayAudio(Location location)
    {
        //Method that tells the audiomanager to play if not active, and to stop if already active
        GetLocation(location);
        if (!emitter.IsActive)
            emitter.Play();
        else
            emitter.Stop();
    }

    public void StopAudio(Location location)
    {
        if (emitter.IsActive)
            emitter.Stop();
        else
        {
            emitter.Play();
        }
        //Debug.Log("StopAudio" + location);
    }
}
