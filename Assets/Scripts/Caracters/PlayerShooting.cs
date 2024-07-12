using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefabBala;  
    public float velocidadBala = 30f; 
    public int maxDisparos = 3;  
    public float tiempoEspera = 2f; 

    private int disparosRestantes;
    private float tiempoUltimoDisparo;

    void Start()
    {
        disparosRestantes = maxDisparos;
        tiempoUltimoDisparo = -tiempoEspera;  
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoUltimoDisparo + tiempoEspera && disparosRestantes > 0)
        {
            Disparar();
        }
    }

    void Disparar()
    {
        Vector3 direccionDisparo = Camera.main.transform.forward;
        GameObject bala = Instantiate(prefabBala, transform.position, Quaternion.identity);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direccionDisparo * velocidadBala;

            Bottle bottleScript = bala.GetComponent<Bottle>();
            if (bottleScript != null)
            {
                bottleScript.SetEnemyReference(FindObjectOfType<Enemy>());
            }
            else
            {
                Debug.LogError("El prefab de bala no tiene el componente Bottle.");
            }
        }
        else
        {
            Debug.LogError("El prefab de bala no tiene componente Rigidbody.");
        }

        tiempoUltimoDisparo = Time.time;
        disparosRestantes--;

        if (disparosRestantes <= 0)
        {
            Debug.Log("No quedan más disparos.");
        }
    }
}



