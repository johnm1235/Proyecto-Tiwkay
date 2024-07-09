using UnityEngine;

public class Bala : MonoBehaviour
{
    public float tiempoDestruccion = 1f;  // Tiempo después del cual la bala se destruirá

    void Start()
    {
        // Llamar al método para destruir la bala después de un tiempo
        Destroy(gameObject, tiempoDestruccion);
    }

    void OnCollisionEnter(Collision collision)
    {
        // No necesitas implementar nada aquí para la colisión si la bala se destruye automáticamente
    }
}
