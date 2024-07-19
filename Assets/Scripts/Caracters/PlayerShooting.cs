using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] prefabBala;

    public float velocidadBala = 30f;
    public int maxDisparos = 3;
    public float tiempoEspera = 2f;

    private int disparosRestantes;
    private float tiempoUltimoDisparo;
    private PlayerInventory inventory; // Referencia al inventario del jugador

    void Start()
    {
        disparosRestantes = maxDisparos;
        tiempoUltimoDisparo = -tiempoEspera;

        // Obtener referencia al inventario del jugador
        inventory = PlayerInventory.Instance; // Asegúrate de que PlayerInventory.Instance esté configurado correctamente
    }

    void Update()
    {
        // Verificar si el jugador puede disparar según el inventario
        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoUltimoDisparo + tiempoEspera && disparosRestantes > 0)
        {
            // Verificar si el jugador tiene al menos una botella para disparar
            if (inventory != null && TieneBotellasDisponibles())
            {
                Disparar();
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
        GameObject bala = Instantiate(prefabBala[0], transform.position, Quaternion.identity);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direccionDisparo * velocidadBala;

            // No necesitamos buscar el script Bottle si no vamos a modificarlo
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


