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
        else
        {
            constantSpeed = rb.velocity.magnitude;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Block"))
        {
            Destroy(collision.gameObject);
            levelManager.RemoveBlock();
        }
    }
}
