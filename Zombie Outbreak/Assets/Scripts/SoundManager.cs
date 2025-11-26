using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Tooltip("El clip de sonido a reproducir al hacer click en un botón.")]
    public AudioClip buttonClickSound;

    [Tooltip("El clip de sonido a reproducir al hacer click para atras en un botón.")]
    public AudioClip buttonBackSound;

    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void PlayButtonBackSound()
    {
        audioSource.PlayOneShot(buttonBackSound);
    }
}
