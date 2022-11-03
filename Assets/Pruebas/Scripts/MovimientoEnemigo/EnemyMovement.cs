using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float rangoDeAlerta;
    public LayerMask capaDelJugador;
    bool estarAlerta;
    public Transform posJugador;
    public float velocidad;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        estarAlerta = Physics.CheckSphere(transform.position, rangoDeAlerta, capaDelJugador);
        if (estarAlerta)
        {
            //transform.LookAt(posJugador);
            Vector3 vectorJugador = new Vector3(posJugador.position.x, transform.position.y, posJugador.position.z);
            //transform.LookAt(vectorJugador);
            //transform.position = Vector3.MoveTowards(transform.position,vectorJugador, velocidad * Time.deltaTime);
            navMeshAgent.SetDestination(vectorJugador);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeAlerta);
    }
}
