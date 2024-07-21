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
        // Cambiar la altura mientras la tecla 'LeftControl' esté presionada
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Agachar();
        }
        else
        {
            Levantar();
        }
    }

    // Función para agachar el personaje
    void Agachar()
    {
        characterController.height = alturaModificada;
        // Ajustar el centro para mantener el personaje centrado al agacharse
        characterController.center = new Vector3(centroInicial.x, centroInicial.y / 2, centroInicial.z);
    }

    // Función para levantar el personaje a su altura inicial
    void Levantar()
    {
        characterController.height = alturaInicial;
        characterController.center = centroInicial;
    }
}
