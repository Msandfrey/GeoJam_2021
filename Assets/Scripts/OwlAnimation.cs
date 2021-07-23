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
    [SerializeField] GameObject OwlIdle;
    [SerializeField] GameObject OwlBody;
    [SerializeField] GameObject OwlHead;
    
    void Start()
    {
        //anim.StopPlayback();
        OwlIdle.gameObject.SetActive(true);
        OwlBody.gameObject.SetActive(false);
        OwlHead.gameObject.SetActive(false);
        owlspeed = wingAnim.speed;
        owlspeed = 0;
        wingAnim.speed = 0;
    }
    public void StartFlying()
    {
        OwlIdle.gameObject.SetActive(false);
        OwlBody.gameObject.SetActive(true);
        OwlHead.gameObject.SetActive(true);
        wingAnim.StopPlayback();
        wingAnim.speed = 1;
        owlspeed = 1;
        wingAnim.Play("OwlAnimationFlight1");
    }
    public void StopFlying()
    {
        OwlIdle.gameObject.SetActive(true);
        OwlBody.gameObject.SetActive(false);
        OwlHead.gameObject.SetActive(false);
        wingAnim.StopPlayback();
        wingAnim.speed = 0;
        owlspeed = 0;
    }
}
