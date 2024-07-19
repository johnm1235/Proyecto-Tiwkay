using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour
{
    public GameObject camaraPrincipal; // Solo una c�mara ahora
    private bool cambioRealizado = false;

    // Start is called before the first frame update
    void Start()
    {
        // Activar la c�mara principal al inicio
        if (camaraPrincipal != null)
        {
            camaraPrincipal.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes eliminar los controles de teclado si ya no necesitas cambiar de c�mara
        // De lo contrario, puedes usar un m�todo similar para otras funcionalidades
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarCamara();
        }
    }

    void ActivarCamara()
    {
        // Aseg�rate de que solo la c�mara principal est� activa
        if (camaraPrincipal != null)
        {
            camaraPrincipal.SetActive(true);
        }
    }
}

