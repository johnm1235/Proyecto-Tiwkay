using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefabBala;

    public float velocidadBala = 30f;
    public float tiempoEspera = 2f;

    private PlayerInventory inventory; // Referencia al inventario del jugador

    private float tiempoUltimoDisparo;

    void Start()
    {
        tiempoUltimoDisparo = -tiempoEspera;

        // Obtener referencia al inventario del jugador
        inventory = PlayerInventory.Instance; // Asegúrate de que PlayerInventory.Instance esté configurado correctamente
    }

    void Update()
    {
        // Verificar si el jugador puede disparar según el inventario
        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoUltimoDisparo + tiempoEspera)
        {
            // Verificar si el jugador tiene al menos una botella para disparar
            if (inventory != null && TieneBotellasDisponibles())
            {
                Disparar();

                // Eliminar una botella del inventario después de disparar
                EliminarUnaBotellaDelInventario();
            }
            else
            {
                Debug.Log("No tienes suficientes botellas en el inventario para disparar.");
            }
        }
    }

    bool TieneBotellasDisponibles()
    {
        foreach (PlayerInventory.Item item in inventory.items)
        {
            if (item.type == ItemType.bottles && item.amount > 0)
            {
                return true;
            }
        }
        return false;
    }

    void Disparar()
    {
        Vector3 direccionDisparo = Camera.main.transform.forward;

        // Crear una bala (prefabBala) en la posición del jugador con rotación por defecto
        GameObject bala = Instantiate(prefabBala, transform.position, Quaternion.identity);
        Rigidbody rb = bala.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = direccionDisparo * velocidadBala;
        }
        else
        {
            Debug.LogError("El prefab de bala no tiene componente Rigidbody.");
        }

        tiempoUltimoDisparo = Time.time;
    }

    void EliminarUnaBotellaDelInventario()
    {
        foreach (PlayerInventory.Item item in inventory.items)
        {
            if (item.type == ItemType.bottles && item.amount > 0)
            {
                // Reducir la cantidad de botellas en el inventario
                item.amount--;

                // Aquí podrías agregar más lógica, como actualizar la UI del inventario

                Debug.Log("Botella disparada eliminada del inventario.");
                return; // Salir del método una vez que se ha eliminado una botella
            }
        }
    }
}
