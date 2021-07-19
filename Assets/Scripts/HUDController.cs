using System;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (ballCountText == null)
        {
            Debug.LogError("Ball count text must be set in HUDController");
        }

        if (scoreText == null)
        {
            Debug.LogError("Score text must be set in HUDController");
        }

        SetScore(0);
        SetBallCount(0);
    }

    public void OpenMenuScreen()
    {
        Debug.LogError("OpenMenuScreen not implemented");
    }

    public void SetScore(int score)
    {
        scoreText.text = score + "";
    }

    public void SetBallCount(int ballCount)
    {
        if (ballCount < 0)
        {
            Debug.Log(String.Format("Attempted to set ball count to below zero ({0}). Defaulting to displaying zero instead.", ballCount));
            ballCount = 0;
        }
        ballCountText.text = ballCount + "";
    }
}
