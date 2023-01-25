using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum FaunoEstados
{
    Patrullando,
    Atacando,
    Persiguiendo,
    Stuned
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
    [SerializeField]
    private Animator _animator;
    //Vida del fauno
    private int Life;
    //Controlador de sonido del fauno
    private FaunoSoundManager faunoSoundManager;
    //Punto de patrulla seleccionado
    public Transform selectedPoint;
    //Lista de items que podría generar al morir
    public GameObject[] itemsSoltados;
    public GameObject moneda;
    public bool stuned;
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
        Life = 3;
        faunoSoundManager = GetComponent<FaunoSoundManager>();
    }

    private void Start()
    {
        _indexPoint = UnityEngine.Random.Range(0, _numPoints-1);
        _enemyPoint = _patrolPoints[_indexPoint];
        selectedPoint = _enemyPoint;
        _navMeshAgent.SetDestination(_enemyPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("Daño", false);
        switch (_estadoActual)
        {
            case FaunoEstados.Patrullando:
                _animator.SetBool("Andando", true);
                if (_estarAlerta)
                {
                    faunoSoundManager.rugido();
                    _estadoActual = FaunoEstados.Persiguiendo;
                }
                break;
            case FaunoEstados.Persiguiendo:
                _animator.SetBool("Andando", true);
                
                if (stuned)
                {
                    _estadoActual = FaunoEstados.Stuned;
                }

                if (!_estarAlerta)
                {
                    _estadoActual = FaunoEstados.Patrullando;
                }
                else
                {
                    _navMeshAgent.SetDestination(_posJugador.position);
                    if (Vector3.Distance(this.transform.position, _posJugador.position) < 3.0f)
                    {
                        _navMeshAgent.isStopped = true;
                        _estadoActual = FaunoEstados.Atacando;
                        transform.LookAt(_posJugador.position);
                        _ataque.Atacar();
                        _animator.SetBool("Andando", false);
                        _animator.SetBool("Atacando", true);

                        faunoSoundManager.atacar();
                    }
                }
                break;
            case FaunoEstados.Atacando:
                if (_ataque.EstadoActual == Ataque.EstadoAtaque.FinAtaque)
                {
                    _animator.SetBool("Andando", true);
                    _animator.SetBool("Atacando", false);
                    _estadoActual = FaunoEstados.Persiguiendo;
                    _navMeshAgent.isStopped = false;
                }
                break;
            case FaunoEstados.Stuned:
                if (!stuned)
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
        int randomPosition = UnityEngine.Random.Range(0, _numPoints-1);
        while (randomPosition == _indexPoint)
            randomPosition = UnityEngine.Random.Range(0, _numPoints-1);
        _indexPoint = randomPosition;
        _enemyPoint = _patrolPoints[_indexPoint];
        selectedPoint= _enemyPoint;
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

    public void recibirDanyoX1()
    {
        Life--;
        StopCoroutine(tiempoStuned());
        StartCoroutine(tiempoStuned());
        if (Life <= 0)
        {
            inicioMuerte();
                //Destroy(this.gameObject);
        }
        else
        {
            _animator.SetBool("Daño", true);
        }
    }

    public void recibirDanyoX2()
    {
        Life-=2;
        StopCoroutine(tiempoStuned());
        StartCoroutine(tiempoStuned());
        if (Life <= 0)
        {
            //Destroy(this.gameObject);
            inicioMuerte();
        }
    }

    IEnumerator tiempoStuned()
    {
        stuned = true;
        yield return new WaitForSeconds(3);
        stuned = false;
    }

    private void inicioMuerte()
    {
        faunoSoundManager.atacar();
        _animator.SetBool("Muerto", true);
        DestruirFauno();
    }

    public void DestruirFauno()
    {
        int itemGenerado = Random.Range(0, itemsSoltados.Length);
        int monedasGeneradas = Random.Range(0, 5);
        Instantiate(itemsSoltados[itemGenerado], this.transform.position, this.transform.rotation);
        for(int i = 0; i < monedasGeneradas; i++)
        {
            Instantiate(moneda, this.transform.position, this.transform.rotation);
        }
        if (Life <=0) Destroy(this.gameObject);
    }
}
