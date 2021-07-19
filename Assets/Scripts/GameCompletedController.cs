using UnityEngine;

[RequireComponent(typeof(SceneChangeManager))]
public class GameCompletedController : MonoBehaviour
{
    private SceneChangeManager sceneChangeManager;

    private void Start()
    {
        sceneChangeManager = GetComponent<SceneChangeManager>();
    }

    public void LoadMainMenu()
    {
        sceneChangeManager.SwitchScene("MainMenuScreen");
    }
}
