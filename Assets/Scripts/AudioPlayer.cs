using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSourceButton;

    public void PlayButtonSound()
    {
        audioSourceButton.Play();
    }
}

