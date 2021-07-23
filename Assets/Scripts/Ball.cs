using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    LevelManager levelManager;
    Rigidbody rb;
    [SerializeField]
    float constantSpeed = 20f;
    bool timerOn = false;
    float timer = 0f;
    [SerializeField]
    float allowedIdleTimeBeforeDeath = 4f;
    [SerializeField]
    bool keepSpeedSameForAllBalls = false;
    public bool GottaSwitchModes;
    public bool DoesBallSwitchOnCollision = true;
    bool GravityOn = true;
    float currentY;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!rb.useGravity)
        {
            rb.velocity = constantSpeed * (rb.velocity.normalized);
        }
        //if ball isn't moving start timer
        if ((transform.position.y == currentY || rb.velocity == Vector3.zero) && !timerOn)
        {
            //set timer
            timerOn = true;
            timer = allowedIdleTimeBeforeDeath;
        }
        //check timer on each call to see if ball is moving again
        if(timerOn && timer > 0)
        {
            if((transform.position.y == currentY || rb.velocity == Vector3.zero))
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timerOn = false;
            }
        }else if(timerOn && timer <= 0)
        {
            if((transform.position.y == currentY || rb.velocity == Vector3.zero))
            {
                levelManager.BallFalls(gameObject);
            }
            else
            {
                timerOn = false;
            }
        }
        /***else
        {
            constantSpeed = rb.velocity.magnitude;
        }**/
    }
    public void ToPeggle()
    {
        if (DoesBallSwitchOnCollision)
        {
            GottaSwitchModes = true;
            GravityOn = true;
        }
        else
        {
            rb.useGravity = true;
        }
    }
    public void ToBreakout()
    {
        if (!keepSpeedSameForAllBalls)
        {
            constantSpeed = rb.velocity.magnitude;
        }
        if (DoesBallSwitchOnCollision)
        {
            GottaSwitchModes = true;
            GravityOn = false;
        }
        else
        {
            rb.useGravity = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destructible block = collision.gameObject.GetComponent<Destructible>();
        if (block)
        {
            int blockHP = block.LoseHP();
            if(blockHP <= 0)
            {
                int blockPoints = block.GetPoints();
                block.Die();
                levelManager.RemoveBlock(blockPoints);
            }
        }
        if (GottaSwitchModes)
        {
            rb.useGravity = GravityOn;
            GottaSwitchModes = false;
        }
        currentY = transform.position.y;
    }
}
