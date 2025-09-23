using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAudio : MonoBehaviour
{
    // TODO audioclip random controller
    [Tooltip("AudioClip for foot steps")]
    public AudioClip sfxFootsteps;
    
    [Tooltip("AudioClip at start jump")]
    public AudioClip sfxJump;
    
    [Tooltip("AudioClip played when landing")] 
    public AudioClip sfxLand;
    
    public AudioSource audioSourceFootsteps;    
    public AudioSource audioSourceOneShots;
   
    void Start()
    {
        // retrieve audioSource
        
    }

    public void PlayJump()
    {
       audioSourceOneShots.PlayOneShot(sfxJump);
    }
    
    public void PlayLand()
    {
        audioSourceOneShots.PlayOneShot(sfxLand);
    }

    public void PlayFootstep()
    {
        audioSourceFootsteps.Play();
    }
}
