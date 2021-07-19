using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCatcher : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;

    private void OnTriggerEnter(Collider collision)
    {
        levelManager.BallFalls(collision.gameObject);
    }
}
