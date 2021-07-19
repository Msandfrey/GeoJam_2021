using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    LevelManager LM;

    private void Start()
    {
        LM = FindObjectOfType<LevelManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Block"))
        {
            Destroy(collision.gameObject);
            LM.RemoveBlock();
        }
    }
}
