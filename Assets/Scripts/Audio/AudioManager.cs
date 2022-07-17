using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int _musicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        _musicIndex = (_musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[_musicIndex];
        audioSource.Play();
    }

    public void AudioSourceStop()
    {
        Debug.Log("T MORT");
        audioSource.pitch *= 0f;
    }
}
