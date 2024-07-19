using System.Collections;
using UnityEngine;

public class Aviso : MonoBehaviour
{
    public GameObject textoCanvas; // Referencia al canvas que contiene el texto
    public GameObject canvasWin;
    public bool seRequiereLlave = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verificar si el jugador tiene una llave en su inventario
            bool tieneLlave = TieneLlave();

            if (tieneLlave)
            {
                // Si el jugador tiene una llave, mostrar el canvas de victoria
                MostrarCanvasWin();
                seRequiereLlave = false; // Ya no se requiere llave
            }
            else if (!tieneLlave && seRequiereLlave)
            {
                // Si el jugador no tiene una llave y se requiere una, mostrar el texto temporal
                MostrarTextoTemporal();
            }
        }
    }


    bool TieneLlave()
    {
        foreach (var item in PlayerInventory.Instance.items)
        {
            if (item.type == ItemType.keys && item.amount > 0)
            {
                return true; // El jugador tiene al menos una llave
            }
        }
        return false; // El jugador no tiene llaves
    }


    void MostrarTextoTemporal()
    {
        // Activar el canvas con el texto
        textoCanvas.SetActive(true);

        // Desactivar el canvas después de 3 segundos
        StartCoroutine(EsperarYDesactivar());
    }

    public void MostrarCanvasWin()
    {
        // Asegurarse de que se activa el canvas de victoria correcto
        canvasWin.SetActive(true);
    }



    IEnumerator EsperarYDesactivar()
    {
        yield return new WaitForSeconds(5f);

        // Desactivar el canvas después de 3 segundos
        textoCanvas.SetActive(false);
    }
}
