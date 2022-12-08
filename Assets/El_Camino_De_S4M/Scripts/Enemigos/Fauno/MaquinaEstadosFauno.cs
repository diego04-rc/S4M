using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum FaunoEstados
{
    Patrullando,
    Atacando,
    Persiguiendo
}

public class MaquinaEstadosFauno : MonoBehaviour
{
    bool _estarAlerta;
    private Transform _posJugador;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _temporalRigidBody;
    private bool _rbActive;
    private FaunoEstados _estadoActual;
    [SerializeField]
    private GameObject _enemyZone;
    [SerializeField]
    private int _numPoints;
    private Transform[] _patrolPoints;
    private Transform _enemyPoint;
    private int _indexPoint;
    private Ataque _ataque;

    // Start is called before the first frame update
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _temporalRigidBody = GetComponent<Rigidbody>();
        _estarAlerta = false;
        _estadoActual = FaunoEstados.Patrullando;
        _patrolPoints = new Transform[_numPoints];
        for (int i = 0; i < _numPoints; i++)
        {
            _patrolPoints[i] = _enemyZone.transform.GetChild(i).gameObject.transform;
        }
        _ataque = GetComponent<Ataque>();
    }

    private void Start()
    {
        _indexPoint = UnityEngine.Random.Range(0, _numPoints);
        _enemyPoint = _patrolPoints[_indexPoint];
        _navMeshAgent.SetDestination(_enemyPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_estadoActual)
        {
            case FaunoEstados.Patrullando:
                if (_estarAlerta)
                {
                    _estadoActual = FaunoEstados.Persiguiendo;
                }
                break;
            case FaunoEstados.Persiguiendo:
                if (!_estarAlerta)
                {
                    _estadoActual = FaunoEstados.Patrullando;
                }
                else
                {
                    _navMeshAgent.SetDestination(_posJugador.position);
                    if (Vector3.Distance(this.transform.position, _posJugador.position) < 10.0f)
                    {
                        _navMeshAgent.isStopped = true;
                        _estadoActual = FaunoEstados.Atacando;
                        _ataque.Atacar();
                    }
                }
                break;
            case FaunoEstados.Atacando:
                if (_ataque.EstadoActual == Ataque.EstadoAtaque.FinAtaque)
                {
                    _estadoActual = FaunoEstados.Persiguiendo;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _estarAlerta = true;
            _posJugador = other.gameObject.transform;
        }
        if (other.CompareTag("PatrolPoint") && !_estarAlerta)
        {
            CalcularPatrulla();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _estarAlerta = false;
            CalcularPatrulla();
        }
    }


    public void CalcularPatrulla()
    {
        int randomPosition = UnityEngine.Random.Range(0, _numPoints);
        while (randomPosition == _indexPoint)
            randomPosition = UnityEngine.Random.Range(0, _numPoints);
        _indexPoint = randomPosition;
        _enemyPoint = _patrolPoints[_indexPoint];
        _navMeshAgent.SetDestination(_enemyPoint.position);
    }

    public void SetEnemyZone(GameObject zone)
    {
        _enemyZone = zone;
    }

    public void SetNumPoints(int num)
    {
        _numPoints = num;
    }
}
