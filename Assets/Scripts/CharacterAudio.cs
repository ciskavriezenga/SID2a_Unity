using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public AudioSource audioSourceWallCollision;
    public AudioSource audioSourceFootstep;
    
    public void PlayWallCollisionSound()
    {
        audioSourceWallCollision.Play();
    }

    public void PlayFootstepSound()
    {
        audioSourceFootstep.Play();
    }
}