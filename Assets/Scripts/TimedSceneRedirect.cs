using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSceneRedirect : MonoBehaviour
{
    public float delayInSeconds = 5.0f;

    private void Start()
    {
        Invoke(nameof(LoadMainMenu), delayInSeconds);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(SceneChangeManager.MAIN_MENU_SCENE);
    }

}
