using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlController : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;
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
    bool moving = true;
    bool isAtBottom = false;
    bool isAtTop = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = speed * Vector3.up * (down ? -1 : 1);

        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(StartDelayForOwl());
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ReverseNoWait()
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
            //down = !down;
            //rb.velocity = speed * Vector3.up * (down ? -1 : 1);
        }
    }
    IEnumerator StartDelayForOwl()
    {
        //Debug.Log("wait");
        //float owlWaitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(maxWaitTime);
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
        audioManager.PlayAudioClip(moveAudioClip);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("i fucking made it hoot");
        rb.velocity = Vector3.zero;
        moving = false;
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
