using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcheroo : MonoBehaviour
{
    LevelManager LM;
    // Start is called before the first frame update
    void Start()
    {
        LM = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;//need to change it so that all balls have no gravity?
            LM.ActivateSwitch();
            gameObject.SetActive(false);
        }
    }
}
