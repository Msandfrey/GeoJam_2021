using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickandHold : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private Vector3 mouseOffset;
    private float mouseZCoordinate;

    private Vector3 GetMouseWorldPos()
    {
        //  pixel coordinates (x, y)
        Vector3 mousePoint = Input.mousePosition;
    
        // z coordinate of game object 
        mousePoint.z = mouseZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDown()
    {
        rb.useGravity = true;
        mouseZCoordinate = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mouseOffset;
    }
}
