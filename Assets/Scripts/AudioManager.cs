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

    private AudioClip activePeggleMusicClip;
    private AudioClip activeBrickBreakerMusicClip;

    private void Start()
    {
        PlayerSettings playerSettings = FindObjectOfType<PlayerSettings>();
        SetVolume(playerSettings.GetVolume());
    }

    public void SetVolume(float volume)
    {
        musicAudioSource.volume = volume;

        if (volume <= 0.0f)
        {
            sfxAudioSource.volume = volume;
        }
        else
        {
            // We want the sfx to be slightly louder than the background music. We could
            // use audio mixing for this, but this is a cheaper and easier hack.
            // This can result in the volumes for background music sfx being the same if the player
            // turns up the volume slider, but that's likely rare since the music clips are loud by default.
            sfxAudioSource.volume = Mathf.Min(volume + .2f, 1.0f);
        }
    }

    public void PlayAudioClip(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("AudioClip not set. Skipping audio.");
        }
        else
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }

    public void PlayMainMenuMusic()
    {
        musicAudioSource.clip = mainMenuMusic;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void PlayBrickBreakerLevelMusic(int level)
    {
        if (activePeggleMusicClip == null)
        {
            // levels are 1-based, but our List of background music is zero-based
            int clipIndex = level - 1;

            if (clipIndex >= levelBrickBreakerMusicClips.Count)
            {
                clipIndex = Random.Range(0, levelBrickBreakerMusicClips.Count - 1);
            }

            activePeggleMusicClip = levelBrickBreakerMusicClips[clipIndex];
        }


        PlayBackgroundMusic(activePeggleMusicClip);
    }

    public void PlayPeggleLevelMusic(int level)
    {
        if (activeBrickBreakerMusicClip == null)
        {
            // levels are 1-based, but our List of background music is zero-based
            int clipIndex = level - 1;

            if (clipIndex >= levelPeggleMusicClips.Count)
            {
                clipIndex = Random.Range(0, levelPeggleMusicClips.Count - 1);
            }

            activeBrickBreakerMusicClip = levelPeggleMusicClips[clipIndex];
        }

        PlayBackgroundMusic(activeBrickBreakerMusicClip);
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
