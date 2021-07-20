using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip mainMenuMusic;

    [Tooltip("Ordered list of background peggle music for levels. First item in the list is played for level 1")]
    public List<AudioClip> levelPeggleMusicClips;

    [Tooltip("Ordered list of background brick breaker music for levels. First item in the list is played for level 1")]
    public List<AudioClip> levelBrickBreakerMusicClips;

    private void Start()
    {
        PlayerSettings playerSettings = FindObjectOfType<PlayerSettings>();
        SetVolume(playerSettings.GetVolume());
    }

    public void SetVolume(float volume)
    {
        musicAudioSource.volume = volume;
        sfxAudioSource.volume = volume;
    }

    public void PlayAudioClip(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayMainMenuMusic()
    {
        musicAudioSource.clip = mainMenuMusic;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayBrickBreakerLevelMusic(int level)
    {
        // levels are 1-based, but our List of background music is zero-based
        int levelIndex = level - 1;

        if (levelIndex >= levelBrickBreakerMusicClips.Count)
        {
            Debug.Log(string.Format("Unable to find brick breaker music for level {0} - defaulting to music clip for level 1.", level));
            levelIndex = 0;
        }

        AudioClip clip = levelBrickBreakerMusicClips[levelIndex];

        PlayBackgroundMusic(clip);
    }

    public void PlayPeggleLevelMusic(int level)
    {
        // levels are 1-based, but our List of background music is zero-based
        int levelIndex = level - 1;

        if (levelIndex >= levelBrickBreakerMusicClips.Count)
        {
            Debug.Log(string.Format("Unable to find peggle music for level {0} - defaulting to music clip for level 1.", level));
            levelIndex = 0;
        }

        AudioClip clip = levelPeggleMusicClips[levelIndex];

        PlayBackgroundMusic(clip);
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        // TODO: Add better transitions between clips - stop + start is too abrupt
        musicAudioSource.Stop();

        musicAudioSource.clip = clip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
}
