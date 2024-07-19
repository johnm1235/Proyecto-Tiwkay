using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Etiqueta del jugador
    public string playerTag = "Player";

    // Tipo de ítem que este objeto representa
    public ItemType itemType;

    // Método llamado cuando otro collider entra en contacto con el collider de este objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en contacto tiene la etiqueta del jugador
        if (other.CompareTag(playerTag))
        {
            // Accede al inventario del jugador
            PlayerInventory playerInventory = PlayerInventory.Instance;

            // Añade el objeto al inventario
            AddToInventory();

            // Destruye el objeto en la escena después de ser recogido
            Destroy(gameObject);
        }
    }

    // Método para añadir el objeto al inventario
    void AddToInventory()
    {
        // Accede al inventario del jugador
        PlayerInventory playerInventory = PlayerInventory.Instance;

        // Itera sobre los ítems del inventario para encontrar el tipo correcto
        foreach (PlayerInventory.Item item in playerInventory.items)
        {
            if (item.type == itemType)
            {
                // Incrementa la cantidad de este tipo de ítem en el inventario
                item.amount++;

                // Aquí podrías hacer otras acciones relacionadas con el ítem, como activar efectos, sonidos, etc.

                // Sale del bucle una vez que se ha encontrado el tipo de ítem
                break;
            }
        }
    }
}