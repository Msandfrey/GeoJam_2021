using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public LevelLoader levelLoader;
    public float displayScreenTimeInSeconds = 3.0f;

    private void Start()
    {
        if (levelLoader == null)
        {
            Debug.LogError("LevelLoader is not set. Title screen will not auto-transition to next scene.");
        }
        else
        {
            Invoke(nameof(LoadMainMenu), displayScreenTimeInSeconds);
        }
    }

    private void LoadMainMenu()
    {
        levelLoader.LoadMainMenu();
    }
}
