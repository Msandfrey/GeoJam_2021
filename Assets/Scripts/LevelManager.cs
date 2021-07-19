using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int score;
    [SerializeField]
    int ballCount = 5;
    int blocksLeft;
    bool win = false;

    SceneChangeManager SM;
    [SerializeField]
    BallController Launcher;

    //temp stuff for now
    public Text tempScoreUI;
    public Text tempBallUI;
    public GameObject tempLoseScreenUI;
    public GameObject tempWinScreenUI;
    public GameObject tempRespawnPoint;

    private void Awake()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        blocksLeft = blocks.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        SM = FindObjectOfType<SceneChangeManager>();
        tempBallUI.text = ballCount.ToString();   
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ActivateSwitch()
    {
        //when the switch is hit call this function
        //will add more to this after we know what it does
    }
    public void RemoveBlock()//take in block type
    {
        blocksLeft--;
        //call function to set score with block type as parameter
        AddScore();
        if(blocksLeft <= 0)
        {
            Win();
        }
    }
    public void BallFalls(GameObject ball)
    {
        //destroy the ball
        Destroy(ball);
        //update the ball count
        ballCount--;
        tempBallUI.text = ballCount.ToString();
        //check if they lost
        if(ballCount < 0 && !win)
        {
            Lose();
            return;
        }
        //reset the launcher
        Launcher.ResetLauncher();
    }
    void AddScore()//add paramtere later
    {
        //calculate score based on block type, later with combo shtuff
        score++;
        tempScoreUI.text = score.ToString();
    }
    public void Win()
    {
        //show win screen
        tempWinScreenUI.SetActive(true);
        //show options after win
        //main menu?
        //continue?
            //initiate the next level
    }
    public void Continue()
    {
        SM.SwitchScene();
    }
    public void Lose()
    {
        //show lose screen
        tempLoseScreenUI.SetActive(true);
        //show options after losing
        //restart?
        //main menu?
        //other?
    }
    public void Restart()
    {
        SM.RestartScene();
    }
}
