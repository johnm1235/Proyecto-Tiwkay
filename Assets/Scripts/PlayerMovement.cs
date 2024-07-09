using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controlador;
    public float Speed = 15f;
    public float Gravity = -10f;
    public float velocidadDeCarrera = 2f; // Ajusta esta variable para controlar la velocidad de carrera
    Vector3 Caida;

    // Referencia a la cámara en primera persona
    public Transform cam;
    public Transform EnElPiso;
    public float DistanciaDelPiso;
    public LayerMask MascaraDelPiso;
    bool EstaEnElPiso;

    void Start()
    {
        // Si no has asignado la cámara en el editor, puedes intentar encontrarla automáticamente
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void Update()
    {
        EstaEnElPiso = Physics.CheckSphere(EnElPiso.position, DistanciaDelPiso, MascaraDelPiso);

        if (EstaEnElPiso && Caida.y < 0)
        {
            Caida.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Obtener la dirección relativa a la cámara
        Vector3 moveDirection = x * cam.right + z * cam.forward;
        Caida.y += Gravity * Time.deltaTime;

        // Normalizar el vector de movimiento si es necesario
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Ajustar la velocidad basada en si se está presionando Shift
        float currentSpeed = Speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= velocidadDeCarrera;
        }

        // Aplicar movimiento con CharacterController
        controlador.Move(moveDirection * currentSpeed * Time.deltaTime);
        controlador.Move(Caida * Time.deltaTime);
    }
}
