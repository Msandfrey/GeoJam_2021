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
    bool peg = true;

    SceneChangeManager SM;
    [SerializeField]
    BallController Launcher;
    [SerializeField]
    GameObject breakoutBar;

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
    public void ActivateSwitch()//for now it just does this simple thing
    {
        peg = !peg;
        //if peg show launcher hide bar
        if (peg)
        {
            Launcher.gameObject.SetActive(true);
            breakoutBar.SetActive(false);
        }
        else//else hide launcher show bar
        {
            Launcher.gameObject.SetActive(false);
            breakoutBar.SetActive(true);
        }
    }
    public void RemoveBlock()//take in block type/points value
    {
        blocksLeft--;
        //update the hitstreak here; have timer for clearing?
        //call function to set score with block type as parameter
        int bonus = CalcHitBonus(1);//get the hitstreak here
        AddScore(1,bonus);
        if(blocksLeft <= 0)
        {
            Win();
        }
    }
    public void BallFalls(GameObject ball)
    {
        //destroy the ball
        Destroy(ball);
        if (win) { return; }
        //update the ball count
        ballCount--;//ball count should be lowered at firing?
        tempBallUI.text = ballCount.ToString();
        //check if they lost
        if(ballCount < 0 && !win)
        {
            Lose();
            return;
        }
        if (!peg)
        {
            ActivateSwitch();
        }
        //reset the launcher
        Launcher.ResetLauncher();
    }
    void AddScore(int blockPoints, int bonusMultiplier)//add paramtere later
    {
        //calculate score based on block type, later with combo shtuff
        score += blockPoints*bonusMultiplier;
        tempScoreUI.text = score.ToString();
    }
    int CalcHitBonus(int hitStreak)
    {
        //if in range 1
        //if in range 2
        //if in range 3
        //return bonus for each
        return 1;
    }
    public void Win()
    {
        //show win screen
        tempWinScreenUI.SetActive(true);
        win = true;
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
