using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacles : MonoBehaviour
{

    /* Code a la Dan Mellott. Thank you! :D*/
    public float minRotation = -90.0f;
    public float maxRotation = 90.0f;

    public float minSpeed;
    public float maxSpeed;

    private float originalRotation;
    private float targetRotation;

    private float randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.position.z;
        targetRotation = GenerateRandomTarget();
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, randomSpeed) * Time.deltaTime);
    }

    private float GenerateRandomTarget()
    {
        return Random.Range(minRotation, maxRotation);
    }
}
