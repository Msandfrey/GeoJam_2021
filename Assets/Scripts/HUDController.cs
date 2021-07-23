using System;
using System.Collections.Generic;
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

    [Header("Level Select Menu Settings")]
    public GameObject levelSelectPanel;
    public TMP_Dropdown levelDropDown;

    [Header("Level Text Settings")]
    public TextMeshProUGUI levelText;

    private SceneChangeManager sceneChangeManager;

    // Used for level select menu screen to easily jump between levels
    private Dictionary<string, int> levelToBuildIndex; 

    private bool won;

    private void Awake()
    {
        sceneChangeManager = GetComponent<SceneChangeManager>();

        levelOverPanel.SetActive(false);
        menuPanel.SetActive(false);
        levelSelectPanel.SetActive(false);

        SetScore(0);
        SetBallCount(0);
    }

    private void Start()
    {
        levelText.text = "Level " + sceneChangeManager.GetCurrentLevel();
        InitLevelDropdownList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (levelSelectPanel.activeSelf)
            {
                CloseLevelSelectScreen();
            }
            else
            {
                OpenLevelSelectScreen();
            }
        }
    }

    public void OpenMenuScreen()
    {
        PauseGame();
        menuPanel.SetActive(true);
    }

    public void CloseMenuScreen()
    {
        menuPanel.SetActive(false);
        ResumeGame();
    }

    public void OpenLevelSelectScreen()
    {
        PauseGame();
        levelSelectPanel.SetActive(true);
    }

    public void CloseLevelSelectScreen()
    {
        levelSelectPanel.SetActive(false);
        ResumeGame();
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

    public void LoadSelectedLevel()
    {
        string selectedLevel = levelDropDown.options[levelDropDown.value].text;
        int sceneIndex = levelToBuildIndex[selectedLevel];

        sceneChangeManager.LoadSceneByIndex(sceneIndex);
    }

    public void SetScore(int score)
    {
        scoreText.text = score + "";
    }

    public void ShowLevelCompletedScreen(bool won)
    {
        PauseGame();

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

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void LoadNextLevel()
    {
        sceneChangeManager.SwitchScene();
    }

    private void InitLevelDropdownList()
    {
        levelToBuildIndex = sceneChangeManager.GetLevelsInBuild();

        levelDropDown.options.Clear();

        foreach (KeyValuePair<string, int> entry in levelToBuildIndex)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = entry.Key;

            levelDropDown.options.Add(option);
        }

    }
}
