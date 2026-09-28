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
    // TODO - add audioSources
    // TODO - add settings, e.g. crossfade duration

    

    
    [System.Serializable]
    public struct AreaMusicMapping
    {
        public AreaType area;
        public AudioClip musicClip;
    }
    
    [SerializeField] private List<AreaMusicMapping> areaMusicList;
    
    

}
