using UnityEngine;
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceButton;

    public void PlayButtonSound()
    {
        audioSourceButton.Play();
    }
}


