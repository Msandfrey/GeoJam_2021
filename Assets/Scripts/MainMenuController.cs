using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerSettings))]
[RequireComponent(typeof(SceneChangeManager))]
public class MainMenuController : MonoBehaviour
{
    public Slider volumeSlider;

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

        volumeSlider.value = playerSettings.GetVolume();
        volumeSlider.onValueChanged.AddListener(HandleVolumeSliderValueChanged);
    }

    private void HandleVolumeSliderValueChanged(float value)
    {
        // TODO: Update AudioManager volume as wel
        playerSettings.SaveVolume(value);
    }
}
