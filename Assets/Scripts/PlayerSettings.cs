using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private const string PlayerPrefKeyVolume = "Volume";
    private const float DefaultVolume = 0.5f;

    [SerializeField]
    [Tooltip("For debug purposes only. Set to true to force all player settings to get cleared each time the script loads.")]
    private bool clearSettings = false;

    private void Start()
    {
        if (clearSettings)
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(PlayerPrefKeyVolume, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(PlayerPrefKeyVolume, DefaultVolume);
    }
}
