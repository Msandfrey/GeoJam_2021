using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlController : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;
    bool up = true;
    Rigidbody rb;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float owlWaitTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = speed * Vector3.up * (up ? 1 : -1);
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator WaitAndReverse()
    {
        yield return new WaitForSeconds(owlWaitTime);
        up = !up;
        rb.velocity = speed * Vector3.up * (up ? 1 : -1);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("i fucking made it hoot");
        rb.velocity = Vector3.zero;
        StartCoroutine(WaitAndReverse());
        levelManager.ActivateSwitch(up ? false : true, up ? true : false);
    }
}
