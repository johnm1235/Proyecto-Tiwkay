using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefabBala;  // Prefab de la bala que se va a disparar
    public float velocidadBala = 30f;  // Velocidad de la bala

    void Update()
    {
        // Disparar cuando se presiona el clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        // Obtener la direcci�n hacia donde apunta la c�mara
        Vector3 direccionDisparo = Camera.main.transform.forward;

        // Instanciar la bala en la posici�n del jugador
        GameObject bala = Instantiate(prefabBala, transform.position, Quaternion.identity);

        // Obtener el componente Rigidbody de la bala y aplicarle velocidad en la direcci�n del disparo
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direccionDisparo * velocidadBala;
        }
        else
        {
            Debug.LogError("El prefab de bala no tiene componente Rigidbody.");
        }
    }
}

