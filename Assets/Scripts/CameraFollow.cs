using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al transform del jugador
    public float sensitivity = 2f;  // Sensibilidad del movimiento de la c�mara
    public float smoothing = 10f;   // Suavizado del movimiento de la c�mara

    private Vector3 offset;  // Distancia inicial entre la c�mara y el jugador
    private Vector3 targetCamPos;  // Posici�n a la que la c�mara debe moverse

    void Start()
    {
        // Calculamos la distancia inicial entre la c�mara y el jugador
        offset = transform.position - player.position;

        // Ocultar el cursor y bloquear su posici�n para evitar que salga de la ventana del juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Calcular la nueva posici�n de la c�mara basada en el movimiento del jugador
        Vector3 moveCamTo = player.position + offset;

        // Aplicar suavizado al movimiento de la c�mara
        transform.position = Vector3.Lerp(transform.position, moveCamTo, smoothing * Time.deltaTime);

        // Rotaci�n de la c�mara basada en el movimiento del rat�n (opcional)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotar la c�mara alrededor del jugador (primer persona)
        transform.RotateAround(player.position, Vector3.up, mouseX);
        transform.RotateAround(player.position, transform.right, -mouseY);
    }
}

