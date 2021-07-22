using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlAnimation : MonoBehaviour
{
    [SerializeField]
    Animator wingAnim;
    [SerializeField]
    Animator headAnim;
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
        owlspeed = wingAnim.speed;
        owlspeed = 0;
        wingAnim.speed = 0;
    }
    public void StartFlying()
    {
        wingAnim.StopPlayback();
        wingAnim.speed = 1;
        owlspeed = 1;
        wingAnim.Play("OwlAnimationFlight1");
    }
    public void StopFlying()
    {
        wingAnim.StopPlayback();
        wingAnim.speed = 0;
        owlspeed = 0;
        wingAnim.Play("OwlIdle");
    }
}
