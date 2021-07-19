using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Transform launcher;
    public float launchRotationSpeed;
    float launcherAngle;

    void Update()
    {
        RotateLauncher();
    }

    void RotateLauncher()
    {
        launcherAngle += Input.GetAxis("Mouse X") * launchRotationSpeed * -Time.deltaTime;
        launcherAngle = Mathf.Clamp(launcherAngle, -90, 90);
        launcher.localRotation = Quaternion.Euler(launcherAngle, 90, -90); // if launcher is pointing down
    }
}
