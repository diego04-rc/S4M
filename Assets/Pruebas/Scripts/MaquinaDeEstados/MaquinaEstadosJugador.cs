using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaEstadosJugador : MonoBehaviour
{
    // ###############################################################
    // Variables Globales a los estados

    // Conjuntos de nombres de Axis para las entradas
    private readonly string[] _AxisSaltar    = { "Jump" };
    private readonly string[] _AxisCaminar   = { "Horizontal2", "Vertical" };
    private readonly string[] _AxisCorrer    = { "Run" };
    private readonly string[] _AxisAtacar    = { "Fire1" };

    // Minimo valor a considerar en las comprobaciones
    private const float _Delta = 0.0001f;

    // Booleano para controlar si los inputs estan activos
    private bool _inputsActivos;

    // Variables de estado
    private JugadorEstado _estadoActual;
    private FabricaDeEstados _estados;

    // Booleanos para controlar las acciones
    private bool _saltando;
    private bool _andando;
    private bool _corriendo;

    // Vector de movimiento
    private Vector3 _vectorMovimiento;

    // Movimiento a aplicar
    private Vector3 _movimientoAplicado;
    private float _movimientoY;

    // Velocidades
    private float _velocidadMinAndando;
    private float _velocidadMaxAndando;
    private float _incrementoVelocidadAndando;
    private float _velocidadMinCorriendo;
    private float _velocidadMaxCorriendo;
    private float _incrementoVelocidadCorriendo;
    private float _velocidadActual;
    private float _velocidadSalto;
    private float _velocidadExtraCaida;

    // Controlador del personaje
    private CharacterController _controladorJugador;

    // Hitbox del ataque
    private Collider _hitboxAtaque;

    // Fin Variables Globales a los estados
    // ###############################################################

    // ###############################################################
    // Getters y Setters de variables

    public JugadorEstado EstadoActual
    {
        get { return _estadoActual; }
        set { _estadoActual = value; }
    }

    public bool Saltando
    {
        get { return _saltando; }
    }

    public bool Andando
    {
        get { return _andando; }
    }

    public bool Corriendo
    {
        get { return _corriendo; }
    }

    public Vector3 VectorMovimiento
    {
        get { return _vectorMovimiento; }
    }

    public float MovimientoY
    {
        get { return _movimientoY; }
        set { _movimientoY = value; }
    }

    public Vector3 MovimientoAplicado
    {
        get { return _movimientoAplicado; }
        set { _movimientoAplicado = value; }
    }

    public float VelocidadMinAndando
    {
        get { return _velocidadMinAndando; }
        set { _velocidadMinAndando = value; }
    }

    public float VelocidadMaxAndando
    {
        get { return _velocidadMaxAndando; }
        set { _velocidadMaxAndando = value; }
    }

    public float IncrementoVelocidadAndando
    {
        get { return _incrementoVelocidadAndando; }
        set { _incrementoVelocidadAndando = value; }
    }

    public float VelocidadMinCorriendo
    {
        get { return _velocidadMinCorriendo; }
        set { _velocidadMinCorriendo = value; }
    }

    public float VelocidadMaxCorriendo
    {
        get { return _velocidadMaxCorriendo; }
        set { _velocidadMaxCorriendo = value; }
    }

    public float IncrementoVelocidadCorriendo
    {
        get { return _incrementoVelocidadCorriendo; }
        set { _incrementoVelocidadCorriendo = value; }
    }

    public float VelocidadActual
    {
        get { return _velocidadActual; }
        set { _velocidadActual = value; }
    }

    public float VelocidadSalto
    {
        get { return _velocidadSalto; }
        set { _velocidadSalto = value; }
    }

    public float VelocidadExtraCaida
    {
        get { return _velocidadExtraCaida; }
        set { _velocidadExtraCaida = value; }
    }

    public CharacterController ControladorJugador
    {
        get { return _controladorJugador; }
    }

    // Fin Getters y Setters de variables
    // ###############################################################

    void Start() { }

    private void Awake()
    {
        // Buscamos el controlador del jugador
        _controladorJugador = GameObject.Find("Cuerpo").GetComponent<CharacterController>();

        // Iniciando los estados
        _estados = new FabricaDeEstados(this);
        _estadoActual = _estados.EnTierra();
        _estadoActual.EntrarEstado();

        // Iniciando otras variables
        _inputsActivos = true;
        _saltando = false;
        _andando = false;
        _corriendo = false;
        _vectorMovimiento = Vector3.zero;
        _movimientoAplicado = Vector3.zero;
        _velocidadMinAndando = 2.0f;
        _velocidadMaxAndando = 4.0f;
        _velocidadMinCorriendo = 5.0f;
        _velocidadMaxCorriendo = 7.0f;
        _velocidadSalto = 8.0f;
        _velocidadExtraCaida = 2.0f;
        _incrementoVelocidadAndando = 1.0f;
        _incrementoVelocidadCorriendo = 1.0f;
        _hitboxAtaque = GameObject.Find("HitZone").GetComponent<Collider>();
    }


    void Update()
    { 

        // Comprobamos los inputs del usuario si estan activos
        if (_inputsActivos)
        {
            // Comprobamos si se ha saltado
            _saltando = false;
            foreach (string axis in _AxisSaltar)
            {
                if (Input.GetAxis(axis) != 0)
                {
                    Saltar();
                    break;
                }
            }

            // Comprobamos si se esta moviendo
            _andando = false;
            foreach (string axis in _AxisCaminar)
            {
                if (Input.GetAxis(axis) != 0)
                {
                    Caminar();
                    break;
                }
            }

            // Comprobamos si se esta corriendo
            _corriendo = false;
            foreach (string axis in _AxisCorrer)
            {
                if (Input.GetAxis(axis) > _Delta)
                {
                    Correr();
                    break;
                }
            }

            // Comprobamos si ha atacado
            foreach (string axis in _AxisAtacar)
            {
                if (Input.GetAxis(axis) > _Delta)
                {
                    Atacar();
                    break;
                }
            }
        }

        // Actualizamos el estado actual
        _estadoActual.UpdateEstado();

        // Aplicamos un posible movimiento vertical en Y
        _movimientoAplicado.y = _movimientoY;

        Debug.Log(_movimientoAplicado);

        // Movemos al personaje segun el movimiento aplicado
        ControladorJugador.Move(_movimientoAplicado * Time.deltaTime);
    }

    // ###############################################################
    // Funciones para manejar los inputs del usuario

    void Saltar()
    {
        _saltando = true;
    }

    void Correr()
    {
        _corriendo = true;
    }

    void Atacar()
    {
        _hitboxAtaque.enabled = true;
    }

    void Caminar()
    {
        _andando = true;
        Vector3 delante = Camera.main.transform.forward * Input.GetAxis("Vertical");
        Vector3 derecha = Camera.main.transform.right * Input.GetAxis("Horizontal2");
        delante.y = 0.0f;
        derecha.y = 0.0f;
        _vectorMovimiento = delante + derecha;
        if (_vectorMovimiento.magnitude > 1)
        {
            _vectorMovimiento = _vectorMovimiento.normalized;
        }
    }

    // Fin Funciones para manejar los inputs del usuario
    // ###############################################################

    // ###############################################################
    // Funciones desactivar y activar los inputs del usuario

    void ActivarInputs()
    { _inputsActivos = true; }

    void DesactivarInputs()
    { _inputsActivos = false; }

    // Fin Funciones desactivar y activar los inputs del usuario
    // ###############################################################
}