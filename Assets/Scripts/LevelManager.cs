using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int score;
    float timer;
    [SerializeField]
    float gainBallScoreValue = 100f;
    float scoreUntilBall;
    [SerializeField]
    int ballCount = 5;
    int blocksLeft;
    [SerializeField]
    float gravity = -9.8f;
    bool win = false;
    //-------------------
    [SerializeField]
    bool peg = true;
    [SerializeField]
    bool unoReverseCard = false;
    //0----------------
    bool timerActive = false;
    [SerializeField]
    bool slowTimeOnSwitch = false;
    [SerializeField]
    bool switchToPeggleOnNoBalls = false;
    [SerializeField]
    float switchLagTime = .2f;
    List<GameObject> activeBalls = new List<GameObject>();
    AudioManager audioManager;
    [SerializeField]
    float ballDropSpeed = 10f;
    [SerializeField]
    Transform BreakoutSpawnPoint;
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
    AudioClip switchToPeggleModeAudioClip;
    [SerializeField]
    AudioClip winLevelAudioClip;
    [SerializeField]
    AudioClip loseLevelAudioClip;
    [SerializeField]
    OwlController owl;
    [SerializeField]
    float timeThreshold;
    [SerializeField]
    int hitStreakThresholdLow;
    [SerializeField]
    int hitStreakThresholdMid;
    [SerializeField]
    int hitStreakMultiplier1 = 1;
    [SerializeField]
    int hitStreakMultiplier2 = 3;
    [SerializeField]
    int hitStreakMultiplier3 = 5;
    [SerializeField] 
    int hitStreak;

    [SerializeField]
    GameObject prefabBall;
    //temp stuff for now
    public GameObject tempOwlThing;

    private void Awake()
    {
        InitializeScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        switchToPeggleOnNoBalls = true;
        HUDCont.SetBallCount(ballCount);

        Debug.Log("Starting level: " + sceneManager.GetCurrentLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
        {
            timer += Time.deltaTime;
            if (timer >= timeThreshold)
            {

                Debug.Log("TIMER RESET");
                hitStreak = 0;
                timer = 0f;
                timerActive = false;
            }
        }
    }
    void InitializeScene()
    {
        //get the number of blocks in the level
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        blocksLeft = blocks.Length;
        //make sure gravity is set proper like
        Physics.gravity = new Vector3(0, gravity, 0);
        //Time is normal 
        Time.timeScale = 1;
        //set score to 0
        score = 0;
        timer = 0;
        //bools set to what they need to be
        win = false;
        peg = true;
        unoReverseCard = false;
        timerActive = false;
        //object stuff
        sceneManager = FindObjectOfType<SceneChangeManager>();

        levelNumber = sceneManager.GetCurrentLevel();

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayPeggleLevelMusic(levelNumber);
        scoreUntilBall = gainBallScoreValue;

        if (HUDCont == null)
        {
            HUDCont = FindObjectOfType<HUDController>();
        }
    }
    public void PlayerSwitch()
    {
        unoReverseCard = !unoReverseCard;
        ActivateSwitch(unoReverseCard ? !peg : peg);
    }
    public void OwlSwitch(bool peggle)//for now it just does this simple thing
    {
        peg = peggle;
        bool isPeggleShown = unoReverseCard ? !peg : peg;
        ActivateSwitch(isPeggleShown);
        if(!isPeggleShown && activeBalls.Count <= 0)
        {
            if (switchToPeggleOnNoBalls)
            {
                //ActivateSwitch(true);//return to peggle
                unoReverseCard = false;
                owl.ReverseNoWait();
            }
            else
            {
                //drop in a blall
                DroppingALoadBall();
            }
            ////if we are gonna switch to breakout with no balls, send owl back immediately
            //owl.ReverseNoWait();
            //unoReverseCard = false;
        }
        //if no balls when switch to breakout swith to peggle and go back
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
            audioManager.PlayAudioClip(switchToPeggleModeAudioClip);
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
            audioManager.PlayAudioClip(switchModeAudioClip);
        }
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
        timer = 0;
        hitStreak++;
        Debug.Log("hit streaks is now: " + hitStreak);
        int bonus = CalcHitBonus(hitStreak); 
        Debug.Log("Bonus: " + bonus);

        AddScore(points,bonus);

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
        bool isPeggleShowing = unoReverseCard ? !peg : peg;
        if(activeBalls.Count <= 0 && !isPeggleShowing && switchToPeggleOnNoBalls)
        {
            //if (switchToPeggleOnNoBalls)
            //{
                //ActivateSwitch(true);//return to peggle
                unoReverseCard = false;
                owl.ReverseNoWait();
            //}
            //else
            //{
                //drop in a blall
                //DroppingALoadBall();
            //}
        }
        //reset the launcher
        //Launcher.ResetLauncher();
    }
    void DroppingALoadBall()
    {
        //this is the same as Shoot in BallController
        BallDecrement();
        GameObject createBall = Instantiate(prefabBall, BreakoutSpawnPoint.position, Quaternion.identity);
        Rigidbody rbBall = createBall.GetComponent<Rigidbody>();
        rbBall.useGravity = false;
        Vector3 direction = (Vector3.left + Vector3.down);
        direction.Normalize();

        rbBall.velocity = direction * ballDropSpeed;
        AddActiveBall(createBall);

    }
    void AddScore(int blockPoints, int bonusMultiplier)//add paramtere later
    {
        //calculate score based on block type, later with combo shtuff
        score += blockPoints*bonusMultiplier;
        HUDCont.SetScore(score);
        scoreUntilBall -= blockPoints * bonusMultiplier;
        CheckToAddNewBall();
    }
    void CheckToAddNewBall()
    {
        //check if scoreuntilball is 0
        if(scoreUntilBall <= 0)
        {
            ballCount++;
            HUDCont.SetBallCount(ballCount);
            scoreUntilBall += gainBallScoreValue;
            CheckToAddNewBall();
        }
    }
    int CalcHitBonus(int hitStreak)
    {
        if (hitStreak <= hitStreakThresholdLow)
        {
            return hitStreakMultiplier1;
        }
        else if (hitStreak <= hitStreakThresholdMid)
        {
            return hitStreakMultiplier2;
        }
        else 
        {
            return hitStreakMultiplier3;
        }
    }
    public void Win()
    {
        //show win screen
        HUDCont.ShowLevelCompletedScreen(true);
        win = true;
        audioManager.StopAllMusic();
        audioManager.PlayAudioClip(winLevelAudioClip);
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
        audioManager.StopAllMusic();
        audioManager.PlayAudioClip(loseLevelAudioClip);
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
