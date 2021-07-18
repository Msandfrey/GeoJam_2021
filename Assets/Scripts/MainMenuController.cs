using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerSettings))]
[RequireComponent(typeof(SceneChangeManager))]
public class MainMenuController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioManager audioManager;

    private PlayerSettings playerSettings;
    private SceneChangeManager sceneChangeManager;

    public void Play()
    {
        sceneChangeManager.SwitchScene();
    }

    private void Start()
    {
        playerSettings = GetComponent<PlayerSettings>();
        sceneChangeManager = GetComponent<SceneChangeManager>();

        float volume = playerSettings.GetVolume();
        volumeSlider.value = volume;
        volumeSlider.onValueChanged.AddListener(HandleVolumeSliderValueChanged);

        audioManager.SetVolume(volume);
        audioManager.PlayMainMenuMusic();
    }

    private void HandleVolumeSliderValueChanged(float value)
    {
        audioManager.SetVolume(value);
        playerSettings.SaveVolume(value);
    }
}
