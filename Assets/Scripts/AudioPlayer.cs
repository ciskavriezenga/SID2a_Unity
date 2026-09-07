using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSourceWallCollision;

    public void PlayWallCollisionSound()
    {
        audioSourceWallCollision.Play();
    }
}

