using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SingleTon { get; private set; }
    AudioSource audioSource;
    private void Awake()
    {
        if (SingleTon != null)
        {
            Destroy(gameObject);
            return;
        }
        SingleTon = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioSource audioSource)
    {
        audioSource?.Play();
    }

    public void StopSound(AudioSource audioSource)
    {
        audioSource?.Stop();
    }
}
