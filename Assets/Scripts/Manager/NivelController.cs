using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NivelController : MonoBehaviour
{
    public int nextSceneId; // ID de la siguiente escena a cargar
    public int previousSceneId; // ID del nivel anterior para volver

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.Instance.SavePlayerData();
            if (GameManager.instance.GetNivelActual() == 2)
            {
                // Usa LoadingScreenManager para volver al nivel anterior
                LoadingScreenManager.Instance.SwitchToScene(previousSceneId);
            }
            else
            {
                // Usa LoadingScreenManager para cargar la siguiente escena con pantalla de carga
                LoadingScreenManager.Instance.SwitchToScene(nextSceneId);
            }
        }
    }
}
