using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Transform launcher;
    public float launchRotationSpeed;
    float launcherAngle;
    private Vector3 mousePosition;
    private Vector3 launcherPosition;
    private Quaternion lookAtPosition;

    void Update()
    {
        RotateLauncher();
    }


    void RotateLauncher()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot_z - 90.0f);
    }
}
