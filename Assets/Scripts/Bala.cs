using UnityEngine;

public class Bala : MonoBehaviour
{
    public float tiempoDestruccion = 1f;  // Tiempo despu�s del cual la bala se destruir�

    void Start()
    {
        // Llamar al m�todo para destruir la bala despu�s de un tiempo
        Destroy(gameObject, tiempoDestruccion);
    }

    void OnCollisionEnter(Collision collision)
    {
        // No necesitas implementar nada aqu� para la colisi�n si la bala se destruye autom�ticamente
    }
}
