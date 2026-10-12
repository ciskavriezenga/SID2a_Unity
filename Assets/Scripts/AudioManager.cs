using System.Collections.Generic;
using System;
using UnityEngine;


/*
 * ISerializationCallbackReceiver --> prevent duplicate areas in inspector 
 * to create a dictionary with no duplicates, see link
 * https://docs.unity3d.com/6000.6/Documentation/ScriptReference/ISerializationCallbackReceiver.html
 */

public class AudioManager : MonoBehaviour
{
    
    [System.Serializable]
    public struct AreaMusicMapping
    {
        public AreaType areaType;
        public AudioClip musicClip;
    }
    
    [SerializeField] private List<AreaMusicMapping> areaMusicList;
    
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    private AudioSource currentPlayingAudioSource;
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void SwitchMusic(AreaType newAreaType)
    {
        AudioClip newAudioClip = retrieveNewAudioClip(newAreaType);
        // TODO - check which Audio source is currently playing
        //      use currentPlayingAudioSource
        // TODO - assign audio clip to the 'not playing audioSource
        // TODO - fade out currently playing Audio source 
        // TODO - fade in other playing Audio source
        
        
    }

    private AudioClip retrieveNewAudioClip(AreaType newAreaType)
    {
        foreach(AreaMusicMapping areaMusicMapping in areaMusicList)
        {
            if (areaMusicMapping.areaType == newAreaType)
            {
                return areaMusicMapping.musicClip;
            }
        }
        // NOTE: return fallback, which is currently the first clip. 
        Debug.LogWarning("No music clip found for area type: " + newAreaType);
        return areaMusicList[0].musicClip;
    }
}
