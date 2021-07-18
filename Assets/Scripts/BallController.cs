using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //Matt-------------------------
    //LevelManager LM;
    //Matt-------------------------
    private Rigidbody rbBall;
    public float thrust = 20f;

    void Start()
    {
        //Matt-------------------------
        //LM = FindObjectOfType<LevelManager>();
        //Matt-------------------------
        rbBall = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // shoot only if ball is NOT in air 
        {

            Shoot();
        }
    }   


    void Shoot()
    {
        rbBall.useGravity = true;
        // launch ball based on mouse position
        rbBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = (Vector3)(Input.mousePosition-screenPoint);
        direction.Normalize();

        rbBall.GetComponent<Rigidbody>().AddForce(direction * thrust, ForceMode.Impulse);
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.Contains("Block"))
        {
            Destroy(col.gameObject);
            //Matt-------------------------
            //LM.RemoveBlock();
            //Matt-------------------------
        }
    }

}
