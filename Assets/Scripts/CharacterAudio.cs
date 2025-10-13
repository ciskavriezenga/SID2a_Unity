using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAudio : MonoBehaviour
{
    [Tooltip("AudioClip at start jump")]
    [SerializeField] private AudioClip sfxJump;
    
    [Tooltip("AudioClip played when landing")] 
    [SerializeField] private AudioClip sfxLand;
    
    // For a random container we need an audio source, thus using a seperate audio source for triggering the footsteps
    [SerializeField] private AudioSource audioSourceFootsteps;    
    [SerializeField] private AudioSource audioSourceOneShotsRandomized;

    [SerializeField] private AudioLowPassFilter randomizedLPF;
    [SerializeField] private float randomLPFRangeLow = 1000.0f; 
    [SerializeField] private float randomLPFRangeHigh = 10000.0f;
     
    
    public void PlayJump()
    {
        PlayRandomizedOneShot(sfxJump);
    }
    
    public void PlayLand()
    {
        audioSourceOneShotsRandomized.PlayOneShot(sfxLand);
    }

    private void PlayRandomizedOneShot(AudioClip clip)
    {
        /* NOTE
         * For the sake of simplicity, using hardcoded values for random range. 
         * Instead, two additional public properties could be added that can be
         * set in the unity inspector window of the script component. 
         */
        audioSourceOneShotsRandomized.pitch = Random.Range(0.9f, 1.1f);
        randomizedLPF.cutoffFrequency = Random.Range(randomLPFRangeLow, randomLPFRangeHigh);        
        audioSourceOneShotsRandomized.PlayOneShot(clip);
    }
    public void PlayFootstep()
    {
        audioSourceFootsteps.Play();
    }
}
