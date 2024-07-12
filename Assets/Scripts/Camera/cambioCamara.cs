using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambioCamara : MonoBehaviour
{
    public GameObject[] camaras;
    private bool cambioRealizado = false;

    // Start is called before the first frame update
    void Start()
    {
        // Inicialmente activamos la primera cámara y desactivamos la segunda
        camaras[0].SetActive(true);
        camaras[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarCamara(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !cambioRealizado)
        {
            ActivarCamara(1);
            StartCoroutine(DesactivarCamaraTrasDelay(1, 2f)); // Desactivar cámara 2 después de 5 segundos
        }
    }

    void ActivarCamara(int indice)
    {
        for (int i = 0; i < camaras.Length; i++)
        {
            camaras[i].SetActive(i == indice);
        }
    }

    IEnumerator DesactivarCamaraTrasDelay(int indice, float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivarCamara(indice);
        cambioRealizado = true; // Marcar que el cambio ya se realizó
    }
}
