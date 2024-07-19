using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NivelController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.Instance.SavePlayerData();
            if (GameManager.instance.GetNivelActual() == 2)
            {
                GameManager.instance.VolverNivel();
            }
            else
            {
                GameManager.instance.CargarSiguienteNivel();
            }
        }
    }

}
