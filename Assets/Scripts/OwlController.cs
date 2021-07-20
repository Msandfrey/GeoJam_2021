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
    float owlWaitTime = 1.5f;
    [SerializeField]
    AudioClip moveAudioClip;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = speed * Vector3.up * (down ? -1 : 1);

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator WaitAndReverse()
    {
        yield return new WaitForSeconds(owlWaitTime);
        down = !down;
        rb.velocity = speed * Vector3.up * (down ? -1 : 1);
        audioManager.PlayAudioClip(moveAudioClip);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("i fucking made it hoot");
        rb.velocity = Vector3.zero;
        StartCoroutine(WaitAndReverse());
        levelManager.ActivateSwitch(down ? false : true, down ? true : false);
    }
}
