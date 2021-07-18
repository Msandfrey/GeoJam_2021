using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;

    public AudioClip mainMenuMusic;

    public void SetVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void PlayMainMenuMusic()
    {
        musicAudioSource.clip = mainMenuMusic;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
}
