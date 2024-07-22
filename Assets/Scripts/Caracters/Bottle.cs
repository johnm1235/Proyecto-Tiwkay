using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;


public class Bottle : MonoBehaviour
{
    // public Enemy enemy;
    public float gravity = 9.8f;
    public AudioSource brakeSource;
    public AudioClip brakeSound;

    public void Start()
    {
        brakeSource.spatialBlend = 1.0f;
        brakeSource.rolloffMode = AudioRolloffMode.Logarithmic;
        brakeSource.minDistance = 1f;
        brakeSource.maxDistance = 10f;
        brakeSource.loop = false;
        brakeSource.clip = brakeSound;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Groundetect") )
        {
            brakeSource.PlayOneShot(brakeSound);
        }
        if (other.gameObject.CompareTag("EnemyContact"))
        {
            brakeSource.PlayOneShot(brakeSound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = FindObjectOfType<Enemy>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            brakeSource.PlayOneShot(brakeSound);
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

    //Duplicar el gravedad de la botellas
    public void SetGravity(float gravity)
    {
        this.gravity = gravity;
    }

    void Update()
    {
        transform.position += Vector3.down * gravity * Time.deltaTime;
    }

}
