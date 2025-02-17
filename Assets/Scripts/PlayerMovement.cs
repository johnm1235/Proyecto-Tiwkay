using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controlador;
    public float Speed = 15f;
    public float Gravity = -10;
    Vector3 Caida;

    // Referencia a la c�mara en primera persona
    public Transform cam;
    public Transform EnElPiso;
    public float DistanciaDelPiso;
    public LayerMask MascaraDelPiso;
    bool EstaEnElPiso;

    void Start()
    {
        // Si no has asignado la c�mara en el editor, puedes intentar encontrarla autom�ticamente
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
            Caida.y = -2;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Obtener la direcci�n relativa a la c�mara
        Vector3 moveDirection = x * cam.right + z * cam.forward;
        Caida.y += Gravity * Time.deltaTime;

        // Normalizar el vector de movimiento si es necesario
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Aplicar movimiento con CharacterController
        controlador.Move(moveDirection * Speed * Time.deltaTime);
        controlador.Move(Caida * Time.deltaTime);
    }
}

