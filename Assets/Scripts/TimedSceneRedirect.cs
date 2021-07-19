using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSceneRedirect : MonoBehaviour
{
    public float delayInSeconds = 5.0f;
    public string sceneNameToLoad;

    private void Start()
    {
        if (!string.IsNullOrWhiteSpace(sceneNameToLoad))
        {
            Invoke(nameof(LoadScene), delayInSeconds);
        }
        else
        {
            Debug.LogError("Scene name to load is not set - unable to redirect to next scene");
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneChangeManager.MAIN_MENU_SCENE);
    }

}
