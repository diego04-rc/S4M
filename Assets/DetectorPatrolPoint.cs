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

    private void Awake()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        patrolPoints = new Transform[numPoints];
        for (int i = 0; i < numPoints; i++) patrolPoints[i] = enemyZone.transform.GetChild(i).gameObject.transform;
    }

    void Start()
    {
        int randomPosition = UnityEngine.Random.Range(0, numPoints - 1);
        enemyPoint = patrolPoints[randomPosition];
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
            int randomPosition = UnityEngine.Random.Range(0, numPoints - 1);
            enemyPoint = patrolPoints[randomPosition];
            navMeshAgent.SetDestination(enemyPoint.position);
        }
    }
}
