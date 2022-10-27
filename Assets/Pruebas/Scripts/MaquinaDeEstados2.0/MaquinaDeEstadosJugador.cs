using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaDeEstadosJugador : MonoBehaviour
{
    //##############################################################
    // Inicio Variables Globales

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

    // Varibles para controlar las distintas velocidades
    // Andando
    [SerializeField] private float _velMinAndando;
    [SerializeField] private float _velMaxAndando;
    [SerializeField] private float _incVelAndando;
    // Corriendo
    [SerializeField] private float _velMinCorriendo;
    [SerializeField] private float _velMaxCorriendo;
    [SerializeField] private float _incVelCorriendo;
    // Salto
    [SerializeField] private float _velSalto;
    [SerializeField] private float _gravedad;
    [SerializeField] private float _incDeCaida;

    // Variables de movimiento
    private Vector3 _vectorInput;
    private Vector3 _movFinal;
    private float _movY;

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
        _vectorInput = Vector3.zero;
        _movFinal = Vector3.zero;
        _movY = 0.0f;
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