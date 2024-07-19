using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour
{
    public GameObject camaraPrincipal; // Solo una cámara ahora
    private bool cambioRealizado = false;

    // Start is called before the first frame update
    void Start()
    {
        // Activar la cámara principal al inicio
        if (camaraPrincipal != null)
        {
            camaraPrincipal.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes eliminar los controles de teclado si ya no necesitas cambiar de cámara
        // De lo contrario, puedes usar un método similar para otras funcionalidades
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarCamara();
        }
    }

    void ActivarCamara()
    {
        // Asegúrate de que solo la cámara principal esté activa
        if (camaraPrincipal != null)
        {
            camaraPrincipal.SetActive(true);
        }
    }
}

