using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAudio : MonoBehaviour
{
    public AudioSource audioSourceWallCollision;
    public AudioSource audioSourceFootstep;
    
    public float footstepPitchMin = 1.0f;
    public float footstepPitchMax = 3.0f;
    public float footstepPitchSpeedImpact = 0.5f;
    public float LPFCutoffMin = 5000.0f;
    public float LPFCutoffMax = 15000.0f;

    
    public void PlayWallCollisionSound()
    {
        audioSourceWallCollision.Play();
    }

    public void PlayFootstepSound(float normalizedSpeed)
    {
        if (normalizedSpeed < 0.0f || normalizedSpeed > 1.0f);
        {
            Debug.LogError("VELOCITY MAGNITUDE IS OUT OF BOUND: " + normalizedSpeed);
        }
        
        float pitchValue = Random.Range(footstepPitchMin, footstepPitchMax);
        float offset = normalizedSpeed * footstepPitchSpeedImpact; 
        audioSourceFootstep.pitch = pitchValue + offset;
        Debug.Log("Pitch value is: " + pitchValue);
        
        audioSourceFootstep.Play();
    }
}