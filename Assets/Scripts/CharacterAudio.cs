using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    // TODO audioclip random controller
    [Tooltip("AudioClip for foot steps")]
    public AudioClip sfxFootsteps;
    
    [Tooltip("AudioClip at start jump")]
    public AudioClip sfxJump;
    
    [Tooltip("AudioClip played when landing")] 
    public AudioClip sfxLand;
    
    public AudioSource audioSource;
   
    void Start()
    {
        // retrieve audioSource
        
    }

    public void PlayJump()
    {
       audioSource.PlayOneShot(sfxJump);
    }
    
    public void PlayLand()
    {
        audioSource.PlayOneShot(sfxLand);
    }

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(sfxFootsteps);
    }
}
