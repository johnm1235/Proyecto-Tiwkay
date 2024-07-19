using System.Collections;
using UnityEngine;

public class Aviso : MonoBehaviour
{
    public GameObject textoCanvas; // Referencia al canvas que contiene el texto

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MostrarTextoTemporal();
        }
    }

    void MostrarTextoTemporal()
    {
        // Activar el canvas con el texto
        textoCanvas.SetActive(true);

        // Desactivar el canvas después de 3 segundos
        StartCoroutine(EsperarYDesactivar());
    }

    IEnumerator EsperarYDesactivar()
    {
        yield return new WaitForSeconds(5f);

        // Desactivar el canvas después de 3 segundos
        textoCanvas.SetActive(false);
    }
}
