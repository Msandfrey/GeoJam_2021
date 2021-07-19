using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    [SerializeField]
    private LevelManager LM;

    private void OnTriggerEnter(Collider collision)
    {
        LM.BallFalls(collision.gameObject);
    }
}
