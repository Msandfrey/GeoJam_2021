using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlController : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;
    [SerializeField]
    OwlAnimation owlAnimator;
    [SerializeField] Animator animator;
    bool down = true;
    Rigidbody rb;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    [Tooltip("Sets the minimum time for the owl to wait before moving. If this is greater than Max Wait Time, then the values are automatically swapped.")]
    [Range(0.0f, 15.0f)]
    float minWaitTime = 1.5f;
    [SerializeField]
    [Tooltip("Sets the maximum time for the owl to wait before moving. If this is greater than Max Wait Time, then the values are automatically swapped.")]
    [Range(0.0f, 15.0f)]
    float maxWaitTime = 1.5f;
    [SerializeField]
    AudioClip moveAudioClip;
    AudioManager audioManager;

    Coroutine CoroutineVar;
    bool moving = false;
    bool isAtBottom = false;
    bool isAtTop = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = speed * Vector3.up * (down ? -1 : 1);

        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(StartDelayForOwl());

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ReverseNoWait()//maybe add var to tell when to increase his speed
    {
        if (!moving)
        {
            StopCoroutine(CoroutineVar);
            if (isAtBottom)
            {
                down = false;
                rb.velocity = speed * Vector3.up;
            }else if (isAtTop)
            {
                down = true;
                rb.velocity = -1 * speed * Vector3.up;
            }
            moving = true;
            //owlAnimator.StartFlying();
            //down = !down;
            //rb.velocity = speed * Vector3.up * (down ? -1 : 1);
        }
    }
    IEnumerator StartDelayForOwl()
    {
        //Debug.Log("wait");
        //float owlWaitTime = Random.Range(minWaitTime, maxWaitTime);
        
        // owl idle for 5 sec
        // maxWaitTime = 5.0f;
        // animator.SetBool("Idle", true);
        yield return new WaitForSeconds(maxWaitTime);
        // animator.SetBool("Idle", false);
        rb.velocity = -1 * speed * Vector3.up;
    }
    IEnumerator WaitAndReverse()
    {
        float owlWaitTime = Random.Range(minWaitTime, maxWaitTime);
        Debug.Log("Setting owl wait time to: " + owlWaitTime);

        yield return new WaitForSeconds(owlWaitTime);
        if (isAtBottom)
        {
            down = false;
            rb.velocity = speed * Vector3.up;
        }
        else if (isAtTop)
        {
            down = true;
            rb.velocity = -1 * speed * Vector3.up;
        }
        //down = !down;
        //rb.velocity = speed * Vector3.up * (down ? -1 : 1);
        moving = true;
        //owlAnimator.StartFlying();
        audioManager.PlayAudioClip(moveAudioClip);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("OwlBounds"))
        {
            Debug.Log("i fucking made it hoot");
            rb.velocity = Vector3.zero;
            moving = false;
            //owlAnimator.StopFlying();
            if (down)
            {
                isAtBottom = true;
                isAtTop = false;
            }
            else
            {
                isAtTop = true;
                isAtBottom = false;
            }
            CoroutineVar = StartCoroutine(WaitAndReverse());
            levelManager.OwlSwitch(!down);
        }
    }
}
