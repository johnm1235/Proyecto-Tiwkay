using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agacharse : MonoBehaviour
{
    public CharacterController characterController;
    public float alturaInicial = 2f;  // Altura inicial del CharacterController
    public float alturaModificada = 1f;  // Altura modificada del CharacterController
    private Vector3 centroInicial;  // Centro inicial del CharacterController

    void Start()
    {
        // Obtener el CharacterController del GameObject si no se asignó explícitamente
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        // Asignar la altura inicial al CharacterController y guardar el centro inicial
        characterController.height = alturaInicial;
        centroInicial = characterController.center;
    }

    void Update()
    {
        // Cambiar la altura cuando se presione la tecla 'C'
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleControllerHeight();
        }
    }

    // Función para cambiar entre la altura inicial y la altura modificada del CharacterController
    void ToggleControllerHeight()
    {
        if (characterController.height == alturaInicial)
        {
            characterController.height = alturaModificada;
            // Ajustar el centro para mantener el personaje centrado al agacharse
            characterController.center = new Vector3(centroInicial.x, centroInicial.y / 2, centroInicial.z);
        }
        else
        {
            characterController.height = alturaInicial;
            characterController.center = centroInicial;
        }
    }
}
