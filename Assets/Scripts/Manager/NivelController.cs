using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NivelController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entr� al trigger es el jugador.
        // Esto se puede hacer de varias maneras, aqu� se verifica por tag.
        if (other.CompareTag("Player"))
        {
            // Llama al m�todo CargarSiguienteNivel del GameManager.
            GameManager.instance.CargarSiguienteNivel();
        }
    }
}
