using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSlideObstacle : MonoBehaviour
{
    /*Code a la Dan Mellott. Thank you! :D*/

    public float maxXDistance = 2.0f;
    public float minXDistance = 2.0f;

    public float minSpeed;
    public float maxSpeed;

    private Vector2 center;
    private Vector2 target;
    private float randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        target = GenerateRandomTarget();
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target, transform.position) < 0.01f)
        {
            target = GenerateRandomTarget();
        }

        float step = randomSpeed * Time.deltaTime;
        float originalZ = transform.position.z;

        Vector2 newPosition = Vector2.MoveTowards(transform.position, target, step);
        transform.position = new Vector3(newPosition.x, newPosition.y, originalZ);
    }

    private Vector2 GenerateRandomTarget()
    {
        float x = center.x + Random.Range(-maxXDistance / 2.0f, maxXDistance / 2.0f);
        float y = center.y + Random.Range(-minXDistance / 2.0f, minXDistance / 2.0f);

        return new Vector2(x, transform.position.y);
    }
}
