using UnityEngine;

public class PlaySoundEffectOnImpact : MonoBehaviour
{
    public AudioClip audioClip;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();        
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.PlayAudioClip(audioClip);
    }
}
