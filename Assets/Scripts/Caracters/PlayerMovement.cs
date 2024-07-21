using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controlador;
    public float Speed = 15f;
    public float Gravity = -10f;
    public float velocidadDeCarrera = 2f;
    public float velocidadDeAgacharse = 1f; // Ajusta este valor según necesites
    Vector3 Caida;

    public Transform cam;
    public Transform EnElPiso;
    public float DistanciaDelPiso;
    public LayerMask MascaraDelPiso;
    bool EstaEnElPiso;

    public AudioSource audioSource;
    public AudioClip audioClipCaminar; // AudioClip para el sonido de caminar
    public AudioClip audioClipCorrer; // AudioClip para el sonido de correr

    public bool puedeMoverse = true;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main.transform;
        }

        audioSource.spatialBlend = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 10f;
        audioSource.loop = true;
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

        // Verifica si el jugador está agachado
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);

        if (isCrouching)
        {
            currentSpeed = velocidadDeAgacharse; // Reduce la velocidad cuando está agachado
                                                 // Detiene cualquier sonido si el jugador está agachado
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= velocidadDeCarrera;
                // Reproduce el sonido de correr solo si el jugador está corriendo
                if (!audioSource.isPlaying || audioSource.clip != audioClipCorrer)
                {
                    audioSource.clip = audioClipCorrer;
                    audioSource.volume = 1f;
                    audioSource.Play();
                }
            }
            else
            {
                // Asegura que el sonido de caminar se reproduzca si el jugador está caminando
                if (moveDirection != Vector3.zero && (!audioSource.isPlaying || audioSource.clip != audioClipCaminar))
                {
                    audioSource.clip = audioClipCaminar;
                    audioSource.volume = 0.3f;
                    audioSource.Play();
                }
            }

            // Detiene cualquier sonido si el jugador no se está moviendo
            if (moveDirection == Vector3.zero && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        controlador.Move(moveDirection * currentSpeed * Time.deltaTime);
        controlador.Move(Caida * Time.deltaTime);
    }


    public void AtacadoPorEnemigo()
    {
        puedeMoverse = false;
    }
}
