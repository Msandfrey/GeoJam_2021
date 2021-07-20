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
    [SerializeField]
    float switchLagTime = .2f;

    SceneChangeManager sceneManager;
    [SerializeField]
    BallController Launcher;
    [SerializeField]
    GameObject breakoutBar;
    [SerializeField]
    HUDController HUDCont;

    //temp stuff for now
    public GameObject tempOwlThing;

    private void Awake()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        blocksLeft = blocks.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneChangeManager>();
        HUDCont.SetBallCount(ballCount);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ActivateSwitch(bool peggle = false, bool breakout = false)//for now it just does this simple thing
    {
        //if not specified it will switch to opposite (covers both false and true)
        if (peggle == breakout)
        {
            peg = !peg;
        }
        else//if they are not the same, peggle value can decide our fate
        {
            peg = peggle;
        }
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
            timerActive = false;
        }

        if(blocksLeft <= 0)
        {
            Win();
        }
    }
    public void BallDecrement()
    {
        ballCount--;
        HUDCont.SetBallCount(ballCount);
    }
    public void BallFalls(GameObject ball)
    {
        //destroy the ball
        Destroy(ball);
        if (win) { return; }
        //check if they lost
        if(ballCount <= 0 && !win)
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
        HUDCont.SetScore(score);
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
        HUDCont.ShowLevelCompletedScreen(true);
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
        HUDCont.ShowLevelCompletedScreen(false);
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
