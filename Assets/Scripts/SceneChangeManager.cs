using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public const string MAIN_MENU_SCENE = "MainMenuScreen";

    [Tooltip("The build scene index of the first level in the game.")]
    public int firstLevelSceneIndex = 2;

    //mode generally Single, but can be Additive to stack scenes on top of each other 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //do something each time scene is loaded or do something on specific scene load
        Debug.Log("scene is loaded");
    }

    public void SwitchScene(string scene = "")
    {
        //if scene is "" then load the next scene
        if(scene == "")
        {
            int next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);
        }
        else
        {
            SceneManager.LoadScene(scene);//current level 1; adjust as things change
        }
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Returns a dictionary of levels for the build. 
    /// </summary>
    /// <returns>A dictionary of levels where the key is the name of the scene and the value is the index of the scene in the build</returns>
    public Dictionary<string, int> GetLevelsInBuild()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        Dictionary<string, int> levelToBuildIndex = new Dictionary<string, int>();

        int levelNumber = 0;

        for (int index = firstLevelSceneIndex; index < sceneCount; index++)
        {
            levelNumber++;

            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(index));
            string levelName = string.Format("Level {0} ({1})", levelNumber, sceneName);

            levelToBuildIndex.Add(levelName, index);

        }

        return levelToBuildIndex;
    }

    public int GetCurrentLevel()
    {
        // We ignore the splash screen/main menu screens when determing the level number
        // We add 1 since the scene list is zero based, but our levels are 1 based
        return SceneManager.GetActiveScene().buildIndex - firstLevelSceneIndex + 1;
    }
}
