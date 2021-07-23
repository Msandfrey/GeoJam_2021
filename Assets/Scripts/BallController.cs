using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //Matt-------------------------
    LevelManager levelManager;
    //Matt-------------------------

    public GameObject prefabBall;
    public Transform shootingPoint;
    private Rigidbody rbBall;
    public float thrust = 20f;
    //public bool ballIsActive = false;

    public AudioClip shootAudioClip;
    private AudioManager audioManager;

    void Start()
    {
        //Matt-------------------------
        levelManager = FindObjectOfType<LevelManager>();
        //Matt-------------------------

        audioManager = FindObjectOfType<AudioManager>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))// && !ballIsActive) // shoot only if ball is NOT in air 
        {
            //ballIsActive = true;
            Shoot();
        }
    }   
    public void ResetLauncher()
    {
        //ballIsActive = false;
    }
    void Shoot()
    {
        //first check if there are balls to fire
        if (levelManager.GetRemainingBalls() > 0 && Time.timeScale != 0)
        {
            levelManager.BallDecrement();
            GameObject createBall = Instantiate(prefabBall, shootingPoint.position, shootingPoint.rotation);
            rbBall = createBall.GetComponent<Rigidbody>();
            // launch ball based on mouse position
            rbBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = (Vector3)(Input.mousePosition-screenPoint);
            direction.Normalize();

            rbBall.GetComponent<Rigidbody>().AddForce(direction * thrust, ForceMode.Impulse);
            levelManager.AddActiveBall(createBall);
        }

        if (audioManager != null && shootAudioClip != null)
        {
            audioManager.PlayAudioClip(shootAudioClip);
        }
    }
}
