using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    LevelManager levelManager;
    Rigidbody rb;
    float constantSpeed;

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
        /***else
        {
            constantSpeed = rb.velocity.magnitude;
        }**/
    }
    public void ToPeggle()
    {
        rb.useGravity = true;
    }
    public void ToBreakout()
    {
        constantSpeed = rb.velocity.magnitude;
        rb.useGravity = false;
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
    }
}
