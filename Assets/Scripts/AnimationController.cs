using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
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
            animator.Play("Bouncer", -1, 0f);
        }
    }
}
