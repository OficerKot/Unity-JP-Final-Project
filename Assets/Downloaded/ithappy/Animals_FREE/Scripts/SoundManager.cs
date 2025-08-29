using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] AudioClip dogBackgrMusic;
    [SerializeField] AudioClip catBackgrMusic;

    public static SoundManager Instance;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayCatMusic()
    {
        aud.PlayOneShot(catBackgrMusic);
    }
    public void PlayDogMusic()
    {
        aud.PlayOneShot(dogBackgrMusic);
    }
}
