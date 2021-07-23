using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private const string PlayerPrefKeyVolume = "Volume";
    private const float DefaultVolume = 0.5f;
    private const string PlayerPrefKeyDropBallBool = "DoesBallDrop";
    private const bool DefaultDoesBallDropValue = true;

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

    public void SaveBallDropBool(bool doesBallDrop)
    {
        int savedVal = doesBallDrop ? 1 : 0;
        PlayerPrefs.SetInt(PlayerPrefKeyDropBallBool, savedVal);
        PlayerPrefs.Save();
    }

    public bool GetBallDropValue()
    {
        int value = PlayerPrefs.GetInt(PlayerPrefKeyDropBallBool, 1);
        switch (value)
        {
            case 0:
                return true;
            case 1:
                return false;
            default:
                return false;
        }
    }
}
