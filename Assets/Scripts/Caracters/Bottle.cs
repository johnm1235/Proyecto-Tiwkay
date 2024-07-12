using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public Enemy enemy;


    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();
            if (hitEnemy != null)
            {
                hitEnemy.ApplyStun();
            }
        }
        else if (enemy != null)
        {
            enemy.DistractToPoint(transform.position);
        }
        Destroy(gameObject);
    }
    public void SetEnemyReference(Enemy enemy)
    {
        this.enemy = enemy;
    }

}
