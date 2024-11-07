using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip; // Assign the BGM clip in the Inspector
    [Range(0f, 3f)] public float volume = 1f; // Volume control, default to 1

    private AudioSource audioSource;

    void Start()
    {
        // Set up the audio source and play the BGM
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;         // Set to loop the BGM
        audioSource.playOnAwake = false; // Disable play on awake to control start
        audioSource.volume = volume;     // Set initial volume
        audioSource.Play();
    }

    void Update()
    {
        // Update the volume in real-time in case it’s changed in the Inspector
        audioSource.volume = volume;
    }

    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void ChangeBGM(AudioClip newClip)
    {
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }

    // Optional method to set the volume programmatically
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0f, 1f);
        audioSource.volume = volume;
    }
}
