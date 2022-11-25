using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectorPatrolPoint : MonoBehaviour
{
    // Start is called before the first frame update
    /*public float rangoDeAlerta;
    public LayerMask capaDelJugador;*/
    //public float velocidad;
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject enemyZone;
    [SerializeField]
    private int numPoints;
    private Transform[] patrolPoints;
    private Transform enemyPoint;
    //Indica el indice seleccionado
    private int indexPoint;

    private void Awake()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        patrolPoints = new Transform[numPoints];
        //Obtenemos todos los puntos de patrulla, siendo estos hijos de la zona enemiga
        for (int i = 0; i < numPoints; i++) patrolPoints[i] = enemyZone.transform.GetChild(i).gameObject.transform;
    }

    void Start()
    {
        indexPoint = UnityEngine.Random.Range(0, numPoints);
        enemyPoint = patrolPoints[indexPoint];
        navMeshAgent.SetDestination(enemyPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        //estarAlerta = Physics.CheckSphere(transform.position, rangoDeAlerta, capaDelJugador);
    }

    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeAlerta);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PatrolPoint") & !this.gameObject.GetComponentInParent<EnemyMovement>().EstaAlerta())
        {
            int randomPosition = UnityEngine.Random.Range(0, numPoints);
            //Si el indice generado es igual al antiguo debe volver a generarse uno nuevo hasta que sean distintos
            while (randomPosition == indexPoint) randomPosition = UnityEngine.Random.Range(0, numPoints);
            indexPoint = randomPosition;
            enemyPoint = patrolPoints[indexPoint];
            navMeshAgent.SetDestination(enemyPoint.position);
        }
    }
}
