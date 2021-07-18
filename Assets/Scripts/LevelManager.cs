using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int score;
    public int ballCount;
    public int blocksLeft;
    public bool win = false;

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
        tempBallUI.text = ballCount.ToString();   
    }

    // Update is called once per frame
    void Update()
    {
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
        //Destroy(ball);
        ballCount--;
        tempBallUI.text = ballCount.ToString();
        if(ballCount < 0)
        {
            Destroy(ball);
            if (!win)
            {
                Lose();
            }
            return;
        }
        //for now don't destroy the ball, just put it back at the top
        ball.transform.position = tempRespawnPoint.transform.position;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().useGravity = false;
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
    public void Lose()
    {
        //show lose screen
        tempLoseScreenUI.SetActive(true);
        //show options after losing
        //restart?
        //main menu?
        //other?
    }
}
