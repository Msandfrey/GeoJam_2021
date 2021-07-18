using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    private void Awake()
    {
        //check to make sure there is only one of these bad boys in play
        if(FindObjectsOfType<SceneChangeManager>().Length > 1)
        {
            Destroy(this);
        }
        //keep persistant throughout the game
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnSceneLoaded(Scene scene)//can 
    {
        //do something each time scene is loaded or do something on specific scene load
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
