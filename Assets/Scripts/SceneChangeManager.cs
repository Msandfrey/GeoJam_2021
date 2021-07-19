using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public const string MAIN_MENU_SCENE = "MainMenuScreen";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)//mode generally Single, but can be Additive to stack scenes on top of each other 
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
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
