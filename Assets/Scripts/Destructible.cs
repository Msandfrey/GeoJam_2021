using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    int HP;
    [SerializeField]
    int points;

    public int GetPoints()
    {
        return points;
    }
    public int LoseHP()
    {
        return --HP;
    }
    public void Die()
    {
        //when it dies die it
        //do any particle efffect or sound stuff here too
        Destroy(gameObject);
    }
}
