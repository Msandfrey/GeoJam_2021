using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBouncer : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Ball"))
        {
            animator.Play("Take 001", -1, 0f);
        }
    }
}
