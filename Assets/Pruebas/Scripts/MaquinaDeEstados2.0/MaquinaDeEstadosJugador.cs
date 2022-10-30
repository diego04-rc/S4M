using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaquinaDeEstadosJugador : MonoBehaviour
{
    //##############################################################
    // Inicio Variables Globales

    // Enumerables con los posibles estados del jugador
    public enum EstadoHoja {AndandoAireCombate, AndandoAire, AndandoCombateFijando, 
        AndandoCombate, Andando, AtacarCombateFijando, AtacarCombate, CorriendoAireCombate,
        CorriendoAire, CorriendoCombateFijando, CorriendoCombate, Corriendo,
        EsquivarCombateFijando, EsquivarCombate, QuietoAireCombate, QuietAire,
        QuietoCombateFijando, QuietoCombate, Quieto};

    public enum EstadoPadre {AireCombate, AireExploracion, TierraCombate, TierraExploracion,
        InteractuandoConEntorno}

    public enum Subestado {CombateLibre, EnemigoFijado, EstadoVacio}

    // Variables con el estado actual
    private EstadoHoja _estadoHojaActual;
    private EstadoPadre _estadoPadreActual;
    private Subestado _subestadoActual;

    // Conjuntos de nombres de Axis para las entradas
    private readonly string[] _AxisSaltar = { "Jump" };
    private readonly string[] _AxisCaminar = { "Horizontal", "Vertical" };
    private readonly string[] _AxisCorrer = { "Run" };
    private readonly string[] _AxisAtacar = { "Fire1" };

    // Variables para controlar el estado del jugador
    private EstadoJugador _estadoActual;
    private FabricaDeEstadosJugador _fabricaEstados;

    

    // Variable para activar y desactivar los inputs del usuario
    private bool _inputsActivos;

    // Variables para almacenar los inputs del usuario
    private bool _saltado;
    private bool _andando;
    private bool _corriendo;
    private bool _atacado;

    // Controlador del personaje jugador
    [Tooltip("Controlador del jugador")]
    [SerializeField]
    private CharacterController _controladorJugador;

    // Modelo del personaje
    [Tooltip("Modelo del jugador")]
    [SerializeField]
    private Transform _modeloPersonaje;

    // Trigger detector de enemigos en el area del personaje
    [Tooltip("Colider zona en la que si hay enemigos el jugador pasará a estar en combate")]
    [SerializeField]
    private DetectorTrigger _detectorEnemigos;

    // Varibles para controlar las distintas velocidades
    // Velocidad Actual
    private float _velActual;
    // Andando
    [SerializeField] private float _velMinAndando;
    [SerializeField] private float _velMaxAndando;
    [SerializeField] private float _incVelAndando;
    [SerializeField] private float _velDirAndando;
    // Corriendo
    [SerializeField] private float _velMinCorriendo;
    [SerializeField] private float _velMaxCorriendo;
    [SerializeField] private float _incVelCorriendo;
    [SerializeField] private float _velDirCorriendo;
    // Salto
    [SerializeField] private float _velSalto;
    [SerializeField] private float _gravedad;
    [SerializeField] private float _incDeCaida;

    // Variables de movimiento
    private Vector3 _vectorInput;
    private Vector3 _movFinal;
    private float _movY;

    // Variable para controlar si hay algun enemigo fijado
    private bool _enemigoFijado;

    // Posicion del enemigo fijado
    private Vector3 _posFijado;

    // Variables para el esquive
    // Direccion
    private Vector3 _dirEsquive;
    // Velocidades
    private float _velActEsquive;
    [SerializeField] private float _velMaxEsquive;
    [SerializeField] private float _decVelEsquive;
    // Bloqueador
    [SerializeField] private float _coolDownEsquive;
    private bool _enCoolDownEsquive;

    // Fin Variables Globales
    //##############################################################

    //##############################################################
    // Inicio Getters y Setters

    public bool InputsActivos
    {
        get { return _inputsActivos; }
        set { _inputsActivos = value; }
    }
    public EstadoJugador EstadoActual
    {
        get { return _estadoActual; }
        set { _estadoActual = value; }
    }
    public FabricaDeEstadosJugador FabricaEstados
    {
        get { return _fabricaEstados; }
        set { _fabricaEstados = value; }
    }
    public bool Saltado
    {
        get { return _saltado; }
        set { _saltado = value; }
    }
    public bool Andando
    {
        get { return _andando; }
        set { _andando = value; }
    }
    public bool Corriendo
    {
        get { return _corriendo; }
        set { _corriendo = value; }
    }
    public bool Atacado
    {
        get { return _atacado; }
        set { _atacado = value; }
    }
    public CharacterController ControladorJugador
    {
        get { return _controladorJugador; }
    }
    public Transform ModeloPersonaje
    {
        get { return _modeloPersonaje; }
    }
    public float VelActual
    {
        get { return _velActual; }
        set { _velActual = value; }
    }
    public float VelMinAndando
    {
        get { return _velMinAndando; }
        set { _velMinAndando = value; }
    }
    public float VelMaxAndando
    {
        get { return _velMaxAndando; }
        set { _velMaxAndando = value; }
    }
    public float IncVelAndando
    {
        get { return _incVelAndando; }
        set { _incVelAndando = value; }
    }
    public float VelDirAndando
    {
        get { return _velDirAndando; }
        set { _velDirAndando = value; }
    }
    public float VelMinCorriendo
    {
        get { return _velMinCorriendo; }
        set { _velMinCorriendo = value; }
    }
    public float VelMaxCorriendo
    {
        get { return _velMaxCorriendo; }
        set { _velMaxCorriendo = value; }
    }
    public float IncVelCorriendo
    {
        get { return _incVelCorriendo; }
        set { _incVelCorriendo = value; }
    }
    public float VelDirCorriendo
    {
        get { return _velDirCorriendo; }
        set { _velDirCorriendo = value; }
    }
    public float VelSalto
    {
        get { return _velSalto; }
        set { _velSalto = value; }
    }
    public float Gravedad
    {
        get { return _gravedad; }
        set { _gravedad = value; }
    }
    public float IncDeCaida
    {
        get { return _incDeCaida; }
        set { _incDeCaida = value; }
    }
    public Vector3 VectorInput
    {
        get { return _vectorInput; }
        set { _vectorInput = value; }
    }
    public Vector3 MovFinal
    {
        get { return _movFinal; }
        set { _movFinal = value; }
    }
    public float MovY
    {
        get { return _movY; }
        set { _movY = value; }
    }
    public bool EnemigoFijado
    {
        get { return _enemigoFijado; }
        set { _enemigoFijado = value; }
    }
    public Vector3 PosFijado
    {
        get { return _posFijado; }
        set { _posFijado = value; }
    }
    public Vector3 DirEsquive
    {
        get { return _dirEsquive; }
        set { _dirEsquive = value; }
    }
    public float VelActEsquive
    {
        get { return _velActEsquive; }
        set { _velActEsquive = value; }
    }
    public float VelMaxEsquive
    {
        get { return _velMaxEsquive; }
        set { _velMaxEsquive = value; }
    }
    public float DecVelEsquive
    {
        get { return _decVelEsquive; }
        set { _decVelEsquive = value; }
    }
    public float CoolDownEsquive
    {
        get { return _coolDownEsquive; }
        set { _coolDownEsquive = value; }
    }
    public bool EnCoolDownEsquive
    {
        get { return _enCoolDownEsquive; }
        set { _enCoolDownEsquive = value; }
    }
    public EstadoHoja EstadoHojaActual
    {
        get { return _estadoHojaActual; }
        set { _estadoHojaActual = value; }
    }
    public EstadoPadre EstadoPadreActual
    {
        get { return _estadoPadreActual; }
        set { _estadoPadreActual = value; }
    }
    public Subestado SubestadoActual
    {
        get { return _subestadoActual; }
        set { _subestadoActual = value; }
    }

    // Fin Getters y Setters
    //##############################################################

    void Awake()
    {
        // Iniciamos las variables de estado
        _fabricaEstados = new FabricaDeEstadosJugador(this);
        _estadoActual = _fabricaEstados.EnTierraExploracion();
        _estadoActual.EntrarEstado();

        // Inputs activos por defecto
        _inputsActivos = true;

        // Inputs a falso por defecto
        _saltado = false;
        _andando = false;
        _corriendo = false;
        _atacado = false;

        // Iniciamos las variables de movimento a cero
        _velActual = 0.0f;
        _vectorInput = Vector3.zero;
        _movFinal = Vector3.zero;
        _movY = 0.0f;

        // Iniciamos que no hay enemigos fijados y su posicion a cero
        _enemigoFijado = false;
        _posFijado = Vector3.zero;

        // Iniciamos la direccion del esquive a cero
        _dirEsquive = Vector3.zero;

        // Esquive no en cooldown
        _enCoolDownEsquive = false;
    }

    void Update()
    {
        // Comprobamos los inputs del usuario si estan activos
        if (_inputsActivos)
        { ComprobarInputs(); }

        // Actualizamos el estado actual
        _estadoActual.UpdateEstado();

        // Actualizamos el movimiento en Y
        _movFinal.y = _movY;

        Debug.Log(_movFinal);

        // Movemos el personaje a partir del movimiento final
        _controladorJugador.Move(_movFinal * Time.deltaTime);
    }

    // Metodo para comprobar los inputs del usuario
    void ComprobarInputs()
    {
        // Comprobamos si se ha saltado
        _saltado = false;
        foreach (string axis in _AxisSaltar)
        {
            if (Input.GetAxis(axis) != 0.0f)
            {
                _saltado = true;
                Saltar();
                break;
            }
        }

        // Comprobamos si se esta moviendo
        _andando = false;
        foreach (string axis in _AxisCaminar)
        {
            if (Input.GetAxis(axis) != 0.0f)
            {
                _andando = true;
                Caminar();
                break;
            }
        }

        // Comprobamos si se esta corriendo
        _corriendo = false;
        foreach (string axis in _AxisCorrer)
        {
            if (Input.GetAxis(axis) != 0.0f)
            {
                _corriendo = true;
                Correr();
                break;
            }
        }

        // Comprobamos si ha atacado
        _atacado = false;
        foreach (string axis in _AxisAtacar)
        {
            if (Input.GetAxis(axis) != 0.0f)
            {
                _atacado = true;
                Atacar();
                break;
            }
        }
    }

    // Metodo para comprobar si hay enemigos en el radio de combate
    public bool EnemigosCerca()
    { return _detectorEnemigos.ObtenerGameObjects().Count > 0; }

    // Metodo para iniciar el cooldown del esquive
    public void IniciarCoolDown()
    {
        StartCoroutine(IniciarCoolDownCorutina());
    }

    // Corutina para iniciar el cooldown del esquive
    private IEnumerator IniciarCoolDownCorutina()
    {
        _enCoolDownEsquive = true;
        yield return new WaitForSeconds(_coolDownEsquive);
        _enCoolDownEsquive = false;
        yield break;
    }

    //##############################################################
    // Metodos para las acciones de los inputs

    void Saltar()
    {
        _saltado = true;
    }

    void Caminar()
    {
        _andando = true;
        Vector3 delante = Camera.main.transform.forward * Input.GetAxis("Vertical");
        Vector3 derecha = Camera.main.transform.right * Input.GetAxis("Horizontal");
        delante.y = 0.0f; derecha.y = 0.0f;
        _vectorInput = delante + derecha;
        if (_vectorInput.magnitude > 1)
        { _vectorInput = _vectorInput.normalized; }
    }

    void Correr()
    {
        _corriendo = true;
    }

    void Atacar()
    {

    }

    // Fin metodos para las acciones de los inputs
    //##############################################################
}