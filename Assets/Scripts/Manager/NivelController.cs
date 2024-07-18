using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NivelController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Si el jugador esta en el nivel 2 y toca el trigger, vuelve al nivel 1
        if (other.CompareTag("Player") && GameManager.instance.GetNivelActual() == 2)
        {
            GameManager.instance.VolverNivel();
        }
        //Sino si el jugador esta en el nivel 1 y toca el trigger, carga el nivel 2
        else
        {
            GameManager.instance.CargarSiguienteNivel();
        }
    }
}
