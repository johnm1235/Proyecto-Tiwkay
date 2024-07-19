using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Etiqueta del jugador
    public string playerTag = "Player";

    // Tipo de �tem que este objeto representa
    public ItemType itemType;

    // M�todo llamado cuando otro collider entra en contacto con el collider de este objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entr� en contacto tiene la etiqueta del jugador
        if (other.CompareTag(playerTag))
        {
            // Accede al inventario del jugador
            PlayerInventory playerInventory = PlayerInventory.Instance;

            // A�ade el objeto al inventario
            AddToInventory();

            // Destruye el objeto en la escena despu�s de ser recogido
            Destroy(gameObject);
        }
    }

    // M�todo para a�adir el objeto al inventario
    void AddToInventory()
    {
        // Accede al inventario del jugador
        PlayerInventory playerInventory = PlayerInventory.Instance;

        // Itera sobre los �tems del inventario para encontrar el tipo correcto
        foreach (PlayerInventory.Item item in playerInventory.items)
        {
            if (item.type == itemType)
            {
                // Incrementa la cantidad de este tipo de �tem en el inventario
                item.amount++;

                // Aqu� podr�as hacer otras acciones relacionadas con el �tem, como activar efectos, sonidos, etc.

                // Sale del bucle una vez que se ha encontrado el tipo de �tem
                break;
            }
        }
    }
}