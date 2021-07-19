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
    [SerializeField]
    float switchLagTime = .2f;

    SceneChangeManager sceneManager;
    [SerializeField]
    BallController Launcher;
    [SerializeField]
    GameObject breakoutBar;

    //temp stuff for now
    public GameObject tempOwlThing;
    public Text tempScoreUI;
    public Text tempBallUI;
    public GameObject tempLoseScreenUI;
    public GameObject tempWinScreenUI;

    private void Awake()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        blocksLeft = blocks.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneChangeManager>();
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
            //slow down time
            Time.timeScale = .1f;
            //show owl
            tempOwlThing.SetActive(true);
            //coroutine for increase size and wait x secs
            StartCoroutine(InitiateSwitchToTheOtherSide());
            Launcher.gameObject.SetActive(false);
            breakoutBar.SetActive(true);
        }
    }
    public IEnumerator InitiateSwitchToTheOtherSide()
    {
        //give players a couple moments to realize the mode is changing
        yield return new WaitForSeconds(switchLagTime);
        Time.timeScale = 1;
        tempOwlThing.SetActive(false);
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
    public void BallDecrement()
    {
        ballCount--;
        tempBallUI.text = ballCount.ToString();
    }
    public void BallFalls(GameObject ball)
    {
        //destroy the ball
        Destroy(ball);
        if (win) { return; }
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
        sceneManager.SwitchScene();
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
        sceneManager.RestartScene();
    }
}
