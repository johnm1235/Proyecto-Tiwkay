using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al transform del jugador
    public float sensitivity = 2f;  // Sensibilidad del movimiento de la cámara
    public float smoothing = 10f;   // Suavizado del movimiento de la cámara

    private Vector3 offset;  // Distancia inicial entre la cámara y el jugador
    private Vector3 targetCamPos;  // Posición a la que la cámara debe moverse

    void Start()
    {
        // Calculamos la distancia inicial entre la cámara y el jugador
        offset = transform.position - player.position;

        // Ocultar el cursor y bloquear su posición para evitar que salga de la ventana del juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Calcular la nueva posición de la cámara basada en el movimiento del jugador
        Vector3 moveCamTo = player.position + offset;

        // Aplicar suavizado al movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, moveCamTo, smoothing * Time.deltaTime);

        // Rotación de la cámara basada en el movimiento del ratón (opcional)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotar la cámara alrededor del jugador (primer persona)
        transform.RotateAround(player.position, Vector3.up, mouseX);
        transform.RotateAround(player.position, transform.right, -mouseY);
    }
}

