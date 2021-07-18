using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    [SerializeField]
    private LevelManager LM;

    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.tag.Equals("Ball"))
        //{
        LM.BallFalls(collision.gameObject);
        //}
    }
}
