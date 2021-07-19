using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int score;
    float timer;
    [SerializeField]
    int ballCount = 5;
    int blocksLeft;
    bool win = false;
    bool peg = true;
    bool timerActive = false;

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
        timerActive = true;
        if (timerActive) 
        {
            timer += Time.deltaTime; // start timer to measure time lapse of subsequent block hits
            int hitStreak = CalcHitStreak(timer); // based on time, it's either 1, 2, 3
            int bonus = CalcHitBonus(hitStreak);// based on hitstreak, it's either, 10, 5, 1
            Debug.Log("Time: " + timer);
            Debug.Log("Hit streak: " + hitStreak);
            Debug.Log("Bonus: " + bonus);

            AddScore(1,bonus);
        }
        
        if(blocksLeft <= 0)
        {
            timerActive = false;
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

    int CalcHitStreak(float time)
    {
        // determine if  hitStreak is high (a.k.a 1), mid (a.k.a. 2), or low (a.k.a 3)
        // depending on time lapse (in sec)
        int hitStreak = 0;
        if (time <= 3.0f)
        {   
            hitStreak = 1;
        }
        else if (time <= 5.0f && time >= 3f) 
        {
            hitStreak = 2;
        }
        else if (time >= 10.0f) 
        {
            hitStreak = 3;
        }
        return hitStreak;
    }

    int CalcHitBonus(int hitStreak)
    {
        switch (hitStreak)
        {
            default:
            case 1: return 10;
            case 2: return 5; 
            case 3: return 1; 
        }
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
