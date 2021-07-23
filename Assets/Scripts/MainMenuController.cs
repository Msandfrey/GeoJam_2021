using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerSettings))]
[RequireComponent(typeof(SceneChangeManager))]
public class MainMenuController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioManager audioManager;
    public Toggle SetDropBallOnBreakoutToggle;

    private PlayerSettings playerSettings;
    private SceneChangeManager sceneChangeManager;

    public void Play()
    {
        sceneChangeManager.SwitchScene();
    }

    private void Start()
    {
        playerSettings = FindObjectOfType<PlayerSettings>();
        sceneChangeManager = FindObjectOfType<SceneChangeManager>();

        float volume = playerSettings.GetVolume();
        volumeSlider.value = volume;
        volumeSlider.onValueChanged.AddListener(HandleVolumeSliderValueChanged);

        SetDropBallOnBreakoutToggle.onValueChanged.AddListener(HandleBreakoutNoBall);

        audioManager.SetVolume(volume);
        audioManager.PlayMainMenuMusic();
    }

    private void HandleBreakoutNoBall(bool dropBall)
    {
        playerSettings.SaveBallDropBool(dropBall);
    }

    private void HandleVolumeSliderValueChanged(float value)
    {
        audioManager.SetVolume(value);
        playerSettings.SaveVolume(value);
    }
}
