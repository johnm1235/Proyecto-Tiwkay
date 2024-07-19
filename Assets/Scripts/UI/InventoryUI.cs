using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory; // Referencia al inventario del jugador
    public Text[] itemAmountTexts; // Referencias a los elementos de texto de UI

    void Start()
    {
        if (playerInventory != null)
        {
            playerInventory.OnInventoryChanged += UpdateUI; // Suscribirse al evento (necesitas implementar este evento en PlayerInventory)
            UpdateUI(); // Actualizar la UI al inicio
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < playerInventory.items.Length; i++)
        {
            if (i < itemAmountTexts.Length)
            {
                itemAmountTexts[i].text = playerInventory.items[i].amount.ToString();
            }
        }
    }
    void OnDestroy()
    {
        playerInventory.OnInventoryChanged -= UpdateUI;
    }
}

