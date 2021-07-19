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
        // launcherAngle += Input.GetAxis("Mouse X") * launchRotationSpeed * -Time.deltaTime;
        // launcherAngle = Mathf.Clamp(launcherAngle, -90, 90);
        // launcher.localRotation = Quaternion.Euler(launcherAngle, 90, -90); // if launcher is pointing down


        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // // launcherPosition = Camera.main.ScreenToWorldPoint(launcher.position);
        // // float singleStep = 1.0f * Time.deltaTime;
        // // Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        // // newDirection = new Vector3(newDirection.x, 0, 0);
        // // Debug.DrawRay(transform.position, newDirection, Color.red);
        // // transform.rotation = Quaternion.LookRotation(newDirection);



        // // mousePosition.x = mousePosition.x - launcherPosition.x;
        // // mousePosition.y = mousePosition.y - launcherPosition.y;
        // // launcherAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        // // transform.rotation = Quaternion.Euler(launcherAngle, 0, 0);

        // transform.right = mousePosition - transform.position;


        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot_z - 90.0f);
    }
}
