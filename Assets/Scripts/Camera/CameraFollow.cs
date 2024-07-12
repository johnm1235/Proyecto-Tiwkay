using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public float sensitivity = 2f;  
    public float smoothing = 10f;  
    public float maxVerticalAngle = 80f; 

    private Vector3 offset;  // Distancia inicial entre la c�mara y el jugador

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


