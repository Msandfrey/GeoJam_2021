using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int score;
    float timer;
    [SerializeField]
    int ballCount = 5;
    int blocksLeft;
    [SerializeField]
    float gravity = -9.8f;
    bool win = false;
    bool peg = true;
    bool unoReverseCard = false;
    bool timerActive = false;
    [SerializeField]
    bool slowTimeOnSwitch = false;
    [SerializeField]
    bool switchToPeggleOnNoBalls = false;
    [SerializeField]
    float switchLagTime = .2f;
    List<GameObject> activeBalls = new List<GameObject>();
    AudioManager audioManager;

    SceneChangeManager sceneManager;
    [SerializeField]
    BallController Launcher;
    [SerializeField]
    GameObject breakoutBar;
    [SerializeField]
    HUDController HUDCont;
    [SerializeField]
    int levelNumber = 1;
    [SerializeField]
    AudioClip switchModeAudioClip;
    [SerializeField]
    OwlController owl;


    //temp stuff for now
    public GameObject tempOwlThing;

    private void Awake()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        blocksLeft = blocks.Length;
        Physics.gravity = new Vector3(0, gravity, 0);
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneChangeManager>();

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayPeggleLevelMusic(levelNumber);

        HUDCont.SetBallCount(ballCount);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PlayerSwitch()
    {
        unoReverseCard = !unoReverseCard;
        ActivateSwitch(!peg);
    }
    public void OwlSwitch(bool peggle)//for now it just does this simple thing
    {
        peg = unoReverseCard ? !peggle : peggle;
        ActivateSwitch(peg);
    }
    void ActivateSwitch(bool peggle)
    {
        //if peg show launcher hide bar
        if (peggle)
        {
            Launcher.gameObject.SetActive(true);
            breakoutBar.SetActive(false);
            //turn gravity on for the ball
            if (activeBalls != null)
            {
                foreach(GameObject ball in activeBalls)
                {
                    ball.GetComponent<Ball>().ToPeggle();
                }
            }
            audioManager.PlayPeggleLevelMusic(levelNumber);
        }
        else//else hide launcher show bar
        {
            //slow down time
            Time.timeScale = slowTimeOnSwitch ? .1f : 1f;
            //show owl
            tempOwlThing.SetActive(true);
            //coroutine for increase size and wait x secs
            StartCoroutine(InitiateSwitchToTheOtherSide());
            Launcher.gameObject.SetActive(false);
            breakoutBar.SetActive(true);
            //turn off gravity of ball
            if (activeBalls != null)
            {
                foreach (GameObject ball in activeBalls)
                {
                    ball.GetComponent<Ball>().ToBreakout();
                }
            }
            audioManager.PlayBrickBreakerLevelMusic(levelNumber);
        }
        audioManager.PlayAudioClip(switchModeAudioClip);

    }
    public IEnumerator InitiateSwitchToTheOtherSide()
    {
        //give players a couple moments to realize the mode is changing
        yield return new WaitForSeconds(switchLagTime);
        Time.timeScale = 1;
        tempOwlThing.SetActive(false);
    }
    public void RemoveBlock(int points)//take in block type/points value
    {
        blocksLeft--;
        timerActive = true;
        if (timerActive)
        {
            timer += Time.deltaTime; // start timer to measure time lapse of subsequent block hits
            int hitStreak = CalcHitStreak(timer); // based on time, it's either 1, 2, 3
            int bonus = CalcHitBonus(hitStreak);// based on hitstreak, it's either, 10, 5, 1
            // Debug.Log("Time: " + timer);
            // Debug.Log("Hit streak: " + hitStreak);
            // Debug.Log("Bonus: " + bonus);

            AddScore(points,bonus);
            timerActive = false;
        }

        if(blocksLeft <= 0)
        {
            Win();
        }
    }
    public void AddActiveBall(GameObject ball)
    {
        activeBalls.Add(ball);
    }
    public void BallDecrement()
    {
        ballCount--;
        HUDCont.SetBallCount(ballCount);
    }
    public int GetRemainingBalls()
    {
        return ballCount;
    }
    public void BallFalls(GameObject ball)
    {
        activeBalls.Remove(ball);
        //destroy the ball
        Destroy(ball);
        if (win) { return; }
        //check if they lost
        if(ballCount <= 0 && !win && activeBalls.Count <= 0)
        {
            Lose();
            return;
        }
        if(activeBalls.Count <= 0 && !peg && switchToPeggleOnNoBalls)
        {
            ActivateSwitch(true);//return to peggle
            owl.ReverseNoWait();
        }
        //reset the launcher
        //Launcher.ResetLauncher();
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
