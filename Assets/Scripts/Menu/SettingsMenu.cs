using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixerMusic;
    public AudioMixer audioMixerSounds;

    public void FullScreen()
    {

    }
    public void SetMusicVolume(float volumeM)
    {
        audioMixerMusic.SetFloat("VolumeMusic", volumeM);
    }
    public void SetSoundVolume(float volumeS)
    {
        audioMixerSounds.SetFloat("VolumeSounds", volumeS);
    }
}
