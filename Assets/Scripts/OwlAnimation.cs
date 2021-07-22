using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlAnimation : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    float owlspeed;
    [SerializeField]
    OwlController owl;
    [SerializeField]
    float flapSpeed = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        //anim.StopPlayback();
        owlspeed = anim.speed;
        owlspeed = 0;
        anim.speed = 0;
    }
    public void StartFlying()
    {
        anim.StopPlayback();
        anim.speed = 1;
        owlspeed = 1;
        anim.Play("OwlAnimationFlight1");
    }
    public void StopFlying()
    {
        anim.StopPlayback();
        anim.speed = 0;
        owlspeed = 0;
        anim.Play("OwlIdle");
    }
}
