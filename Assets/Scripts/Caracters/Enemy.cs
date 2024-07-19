using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public Animator anim;

    // Variables para detecci�n y ataque del jugador
    public GameObject target;
    public bool atacando;
    private bool jugadorDetectado = false;
    public float MaxVisionDistance;
    public float MinVisionDistance;

    // Cuando se tira una botella, el enemigo se distrae y se dirige hacia ella
    public float investigationTime;
    private bool isInvestigating = false;
    private float investigationTimer;
    private Vector3 distractionPoint;
    public float distanciaDeParada;

    public NavMeshAgent agente;

    // Posiciones para el movimiento del enemigo
    public Transform[] waypoints;
    public int waypointIndex = 0;

    //Variables para cuando el enemigo est� aturdido
    public float stunDuration = 2.0f; 
    private bool isStunned = false;
    private float stunTimer;

    //Variable para saber si el enemigo est� persiguiendo al jugador
    private bool isChasingPlayer = false;

    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip roarSound;

    public AudioSource audioSource;


    private void Start()
    {
        audioSource.spatialBlend = 1.0f; 

        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.minDistance = 1f; 
        audioSource.maxDistance = MaxVisionDistance; 

        audioSource.loop = true;

        anim = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();

        // Suscribirse a los eventos del AiSensor
        AiSensor sensor = GetComponent<AiSensor>();
        if (sensor != null)
        {
            sensor.onPlayerDetected.AddListener(OnPlayerDetected);
            sensor.onPlayerLost.AddListener(OnPlayerLost);
        }
    }


    private void Update()
    {
        if (atacando && !isAttackingCoroutineRunning)
        {
            StartCoroutine(AtaqueYReinicio());
            atacando = false;
        }

        else
        {
            if (isStunned)
            {
                StunTime();
            }
            else
            {
                float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);
                if (distanciaAlJugador <= MinVisionDistance && !jugadorDetectado)
                {
                    OnPlayerDetected(); // Llamar a OnPlayerDetected si el jugador est� dentro del rango de visi�n y a�n no ha sido detectado
                }
                ComportamientoEnemigo();
            }
        }
    }

    private bool isAttackingCoroutineRunning = false;

    IEnumerator AtaqueYReinicio()
    {
        if (isAttackingCoroutineRunning)
            yield break;  // Salir si la corrutina ya est� en ejecuci�n

        isAttackingCoroutineRunning = true;
        yield return new WaitForSeconds(1);

        GameManager.instance.ReiniciarNivel();

        isAttackingCoroutineRunning = false;
    }


    private void ComportamientoEnemigo()
    {
        if (isInvestigating)
        {
            Investigar();
        }
        else if (!jugadorDetectado)
        {
            Patrullar();
        }
        else if (jugadorDetectado)
        {
            PerseguirJugador();
        }
        ControlarAnimaciones();
    }

    private void Investigar()
    {
        investigationTimer -= Time.deltaTime;
        if (investigationTimer <= 0)
        {
            isInvestigating = false;
        }
    }

    private void Patrullar()
    {
        if (!agente.pathPending && agente.remainingDistance < 2f)
        {
            MoveToNextWaypoint();
        }
    }

    private void PerseguirJugador()
    {
        isChasingPlayer = true;
        if (!audioSource.isPlaying || audioSource.clip != runSound)
        {
            audioSource.clip = runSound;
            audioSource.Play();
        }

        isChasingPlayer = true;
        agente.SetDestination(target.transform.position);
        float distanciaJugador = Vector3.Distance(transform.position, target.transform.position);
        if (distanciaJugador > agente.stoppingDistance)
        {
            anim.SetBool("walk", false);
            anim.SetBool("attack", false);
            anim.SetBool("run", true);
            agente.isStopped = false;
        }
        else
        {
            agente.isStopped = true;
            anim.SetBool("run", false);
            //audioSource.loop = false;




            AtaqueIniciado();
        }
    }

    private void ControlarAnimaciones()
    {
        float speed = agente.velocity.magnitude;
        bool isWalking = speed >= 1f;
        anim.SetBool("walk", isWalking);

        // Reproducir sonido de pasos o correr
        if (isWalking && !audioSource.isPlaying)
        {
            audioSource.clip = walkSound;
            audioSource.Play();
        }
        else if (!isWalking && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void AtaqueIniciado()
    {
        atacando = true;
        // Solo reproducir sonido de rugido si no est� actualmente reproduci�ndose
        if (!audioSource.isPlaying || audioSource.clip != roarSound)
        {
            audioSource.clip = roarSound;
            audioSource.Play();
        }
        anim.SetBool("attack", true);
        anim.SetBool("run", false);
        anim.SetBool("walk", false);

        target.GetComponent<PlayerMovement>().AtacadoPorEnemigo();
    }

    public void OnPlayerDetected()
    {

        Debug.Log("Jugador detectado");
        jugadorDetectado = true;
        agente.speed = 12f;
        agente.acceleration = 35f;
        agente.stoppingDistance = distanciaDeParada;
    }

    public void OnPlayerLost()
    {
        Debug.Log("Jugador perdido");
        jugadorDetectado = false;
        agente.speed = 7f;
        agente.acceleration = 10f;
        agente.stoppingDistance = 0;
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
        isChasingPlayer = false; 
    }

    public void DistractToPoint(Vector3 point)
    {
        // Solo distraer si no est� persiguiendo al jugador
        if (!isChasingPlayer)
        {
            distractionPoint = point;
            isInvestigating = true;
            investigationTimer = investigationTime;
            MoveToPoint(point);
        }
    }

    private void MoveToPoint(Vector3 point)
    {
        if (agente != null)
        {
            agente.SetDestination(point);
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agente.destination = waypoints[waypointIndex].position;
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }

    private void StunTime()
    {
        stunTimer -= Time.deltaTime;
        if (stunTimer <= 0)
        {
            isStunned = false;
            anim.SetBool("stunned", false);
            agente.isStopped = false;
        }
    }

    public void ApplyStun()
    {
        isStunned = true;
        stunTimer = stunDuration;
        anim.SetBool("stunned", true);
        agente.isStopped = true;
    }
}
