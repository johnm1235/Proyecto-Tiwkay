using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f;
    public float smoothing = 10f;
    public float maxVerticalAngle = 80f;
    public float standingHeight = 1.5f;  // Altura de la c�mara cuando est� de pie
    public float crouchingHeight = 0.75f; // Altura de la c�mara cuando est� agachado
    public KeyCode crouchKey = KeyCode.C; // Tecla para agacharse

    private Vector3 offset;  // Distancia inicial entre la c�mara y el jugador
    private bool isCrouching = false; // Estado actual de si est� agachado o no

    void Start()
    {
        offset = transform.position - player.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Calcular la nueva posici�n de la c�mara basada en el movimiento del jugador
        Vector3 moveCamTo = player.position + offset;

        // Aplicar suavizado al movimiento de la c�mara
        transform.position = Vector3.Lerp(transform.position, moveCamTo, smoothing * Time.deltaTime);

        // Cambiar altura seg�n el estado de agacharse
        float targetHeight = isCrouching ? crouchingHeight : standingHeight;

        // Interpolar la altura de la c�mara
        Vector3 newPosition = transform.position;
        newPosition.y = Mathf.Lerp(transform.position.y, player.position.y + targetHeight, smoothing * Time.deltaTime);
        transform.position = newPosition;

        // Control de agacharse con tecla C
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Limitar el �ngulo vertical de la c�mara
        float desiredAngle = transform.eulerAngles.x - mouseY;
        if (desiredAngle > 180f)
        {
            desiredAngle -= 360f;
        }
        desiredAngle = Mathf.Clamp(desiredAngle, -maxVerticalAngle, maxVerticalAngle);
        mouseY = transform.eulerAngles.x - desiredAngle;

        transform.RotateAround(player.position, Vector3.up, mouseX);
        transform.RotateAround(player.position, transform.right, -mouseY);
    }
}




