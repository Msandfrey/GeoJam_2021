using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SceneChangeManager))]
public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI scoreText;

    public GameObject menuPanel;

    private SceneChangeManager sceneChangeManager;

    private void Awake()
    {
        // Paranoia checks. Since we're using a prefab, we should never have these null checks pass
        // but just in case....
        if (ballCountText == null)
        {
            Debug.LogError("Ball count text must be set in HUDController");
        }

        if (scoreText == null)
        {
            Debug.LogError("Score text must be set in HUDController");
        }

        if (menuPanel == null)
        {
            Debug.LogError("MenuPanel must be set in HUDController");
        }

        sceneChangeManager = GetComponent<SceneChangeManager>();

        menuPanel.SetActive(false);
        SetScore(0);
        SetBallCount(0);
    }

    public void OpenMenuScreen()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
    }

    public void CloseMenuScreen()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        sceneChangeManager.RestartScene();
    }

    public void LoadMainMenu()
    {
        sceneChangeManager.SwitchScene(SceneChangeManager.MAIN_MENU_SCENE);
    }

    public void SetScore(int score)
    {
        scoreText.text = score + "";
    }

    public void SetBallCount(int ballCount)
    {
        if (ballCount < 0)
        {
            Debug.Log(String.Format("Attempted to set ball count to below zero ({0}). Defaulting to displaying zero instead.", ballCount));
            ballCount = 0;
        }
        ballCountText.text = ballCount + "";
    }
}
