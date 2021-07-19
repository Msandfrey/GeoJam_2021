using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutBar : MonoBehaviour
{
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, transform.position.y, 0);
    }
}
