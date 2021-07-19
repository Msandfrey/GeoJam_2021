using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    LevelManager LM;
    Rigidbody rb;
    float constantSpeed;

    private void Start()
    {
        LM = FindObjectOfType<LevelManager>();
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
            LM.RemoveBlock();
        }
    }
}
