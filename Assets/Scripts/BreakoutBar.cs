using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutBar : MonoBehaviour
{
    Vector2 mousePos;
    [SerializeField] public float initialBallSpeed;
    [SerializeField] public float ballAngleOffset;

    void Start()
    {
        
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, transform.position.y, 0);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Ball"))
        {
            Rigidbody rbBall = col.gameObject.GetComponent<Rigidbody>();
            // get ball hit point
            Vector3 hitPoint = col.contacts[0].point;
            
            // get center of bar
            Vector3 barCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y); 

            rbBall.velocity = Vector3.zero;

            // measures how far the ball's hit is from the center of the bar
            float diff = barCenter.x - hitPoint.x; 

            // hitting the left side of the bar
            if (hitPoint.x < barCenter.x) 
            {
                rbBall.AddForce(new Vector2(-(Mathf.Abs(diff * ballAngleOffset)), initialBallSpeed));

            }
            // not hitting the left side (so either right, or near center)
            else 
            {
                rbBall.AddForce(new Vector2((Mathf.Abs(diff * ballAngleOffset)), initialBallSpeed));
            }
        }
    }
}
