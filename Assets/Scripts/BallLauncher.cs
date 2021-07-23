using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    void Update()
    {
        if(Time.timeScale != 0)
        {
            RotateLauncher();
        }
    }

    void OnMouseOver()
    {
        Debug.Log(this.gameObject.name);
    }


    void RotateLauncher()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotateOnX = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotateOnX - 90.0f);
    }
}
