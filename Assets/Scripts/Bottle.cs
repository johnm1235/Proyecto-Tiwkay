using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public Enemy enemy; 

    void OnCollisionEnter(Collision collision)
    {
        if (enemy != null)
        {
            enemy.DistractToPoint(transform.position);
         //   Debug.Log("Distracted" + transform.position);
        }
        Destroy(gameObject);
    }
}
