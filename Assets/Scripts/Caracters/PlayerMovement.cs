using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controlador;
    public float Speed = 15f;
    public float Gravity = -10f;
    public float velocidadDeCarrera = 2f; 
    Vector3 Caida;

    public Transform cam;
    public Transform EnElPiso;
    public float DistanciaDelPiso;
    public LayerMask MascaraDelPiso;
    bool EstaEnElPiso;


    public bool puedeMoverse = true;

    void Start()
    {

        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void Update()
    {
        if (!puedeMoverse) 
        {
            return;
        }

        EstaEnElPiso = Physics.CheckSphere(EnElPiso.position, DistanciaDelPiso, MascaraDelPiso);

        if (EstaEnElPiso && Caida.y < 0)
        {
            Caida.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 moveDirection = x * cam.right + z * cam.forward;
        Caida.y += Gravity * Time.deltaTime;


        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }


        float currentSpeed = Speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= velocidadDeCarrera;
        }

        controlador.Move(moveDirection * currentSpeed * Time.deltaTime);
        controlador.Move(Caida * Time.deltaTime);
    }


    public void AtacadoPorEnemigo()
    {
        puedeMoverse = false;

    }
}

