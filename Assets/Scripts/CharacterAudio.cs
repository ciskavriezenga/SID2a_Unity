using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public AudioSource audioSourceWallCollision;

    public void PlayWallCollisionSound()
    {
        audioSourceWallCollision.Play();
    }
}