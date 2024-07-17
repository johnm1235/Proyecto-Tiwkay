using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agacharse : MonoBehaviour
{
    public BoxCollider collider;
    public float alturaInicial = 2f;  // Altura inicial del collider
    public float alturaModificada = 1f;  // Altura modificada del collider

    void Start()
    {
        // Obtener el BoxCollider del GameObject si no se asignó explícitamente
        if (collider == null)
        {
            collider = GetComponent<BoxCollider>();
        }

        // Asignar la altura inicial al BoxCollider
        collider.size = new Vector3(collider.size.x, alturaInicial, collider.size.z);
    }

    void Update()
    {
        // Ejemplo: Cambiar la altura cuando se presione la tecla 'H' (puedes cambiar esto según tu necesidad)
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleColliderHeight();
        }
    }

    // Función para cambiar entre la altura inicial y la altura modificada del collider
    void ToggleColliderHeight()
    {
        if (collider.size.y == alturaInicial)
        {
            collider.size = new Vector3(collider.size.x, alturaModificada, collider.size.z);
        }
        else
        {
            collider.size = new Vector3(collider.size.x, alturaInicial, collider.size.z);
        }
    }
}