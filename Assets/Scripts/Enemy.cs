using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public Animator anim;

    // Variables para detección y ataque del jugador
    public GameObject target;
    public bool atacando;
    private bool jugadorDetectado = false;
    public float MaxVisionDistance;

    // Cuando se tira una botella, el enemigo se distrae y se dirige hacia ella
    public float investigationTime = 5.0f;
    private bool isInvestigating = false;
    private float investigationTimer;
    private Vector3 distractionPoint;
    public float distanciaDeParada = 3f;

    // NavMeshAgent para el movimiento del enemigo
    public NavMeshAgent agente;

    // Posiciones para el movimiento del enemigo
    public Transform[] waypoints;
    public int waypointIndex = 0;

    

    private void Start()
    {
        anim = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();
        
        MoveToNextWaypoint();

    }


    private void Update()
    {
        ComportamientoEnemigo();
        VerificarRangoVision();

    }

    public void ComportamientoEnemigo()
    {
        // Si el enemigo está investigando, disminuye el tiempo de investigación
        if (isInvestigating)
        {
         //   agente.enabled = true;
            investigationTimer -= Time.deltaTime;
            if (investigationTimer <= 0)
            {
                isInvestigating = false;
            }
            AtaqueTerminado();
        }
        // Si el jugador no es detectado, el enemigo se mueve entre waypoints
        else if (!jugadorDetectado)
        {
            if (!agente.pathPending && agente.remainingDistance < 2f)
            {
                MoveToNextWaypoint();
            }
        }
        // Si el jugador es detectado, el enemigo corre hacia el jugador y lo ataca
        else if (jugadorDetectado)
        {
            agente.SetDestination(target.transform.position);


            // Control de animaciones basado en la velocidad del agente
            if (agente.remainingDistance > agente.stoppingDistance)
            {
                anim.SetBool("walk", false);
                anim.SetBool("attack", false);
                anim.SetBool("run", true);
                agente.isStopped = false; 
            }
            else 
            {
         
                anim.SetBool("run", false);
                AtaqueIniciado();
                agente.isStopped = true; 
            }
        }


        // Control de animaciones basado en la velocidad del agente
        float speed = agente.velocity.magnitude;
        if (speed < 1f) anim.SetBool("walk", false);
        else anim.SetBool("walk", true);
    }

    public void VerificarRangoVision()
    {
        if (jugadorDetectado)
        {
            float distanciaJugador = Vector3.Distance(transform.position, target.transform.position);
            if (distanciaJugador > MaxVisionDistance)
            {
                OnPlayerLost();
            }
        }
    }

    public void AtaqueIniciado()
    {
        anim.SetBool("attack", true);
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
    }

    public void AtaqueTerminado()
    {
        atacando = false;
        anim.SetBool("attack", false);
    }

    public void OnPlayerDetected()
    {
        jugadorDetectado = true;
        agente.speed =7;
        agente.stoppingDistance = distanciaDeParada;
    }

    public void OnPlayerLost()
    {
        jugadorDetectado = false;
        agente.speed = 3.5f;
        agente.stoppingDistance = 0;
        anim.SetBool("run", false);
        anim.SetBool("walk", false); 
        anim.SetBool("attack", false);
    }

    public void DistractToPoint(Vector3 point)
    {
        distractionPoint = point;
        isInvestigating = true;
        investigationTimer = investigationTime;
        MoveToPoint(point);
    }

    void MoveToPoint(Vector3 point)
    {
        if (agente != null)
        {
            agente.SetDestination(point);
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        // Establece el destino del agente al waypoint actual
        agente.destination = waypoints[waypointIndex].position;

        // Incrementa el índice, volviendo al inicio si es necesario
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }

}


