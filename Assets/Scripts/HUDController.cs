using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SceneChangeManager))]
public class HUDController : MonoBehaviour
{
    [Header("Basic HUD Components")]
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI scoreText;

    [Header("Level Over Components")]
    public GameObject levelOverPanel;
    public TextMeshProUGUI levelOverResultText;
    public TextMeshProUGUI levelOverScoreText;

    [Header("Menu Components")]
    public GameObject menuPanel;

    private SceneChangeManager sceneChangeManager;

    private bool won;

    private void Awake()
    {
        sceneChangeManager = GetComponent<SceneChangeManager>();

        levelOverPanel.SetActive(false);
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

    public void LoadNextScene()
    {
        if (won)
        {
            LoadNextLevel();
        }
        else
        {
            RestartLevel();
        }
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

    public void ShowLevelCompletedScreen(bool won)
    {
        this.won = won;

        if (won)
        {
            levelOverResultText.text = "Victory!";
        }
        else
        {
            levelOverResultText.text = "Oops - Try Again";
        }

        levelOverScoreText.text = scoreText.text;

        levelOverPanel.SetActive(true);
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

    private void LoadNextLevel()
    {
        sceneChangeManager.SwitchScene();
    }
}
