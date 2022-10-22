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

    // Movimiento aplicado
    private Vector3 _movimientoAplicado;
    private float _movimientoY;

    // Controlador del personaje
    private CharacterController _controladorJugador;

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

    }

    void Caminar()
    {
        _andando = true;
        _vectorMovimiento = new Vector3(Input.GetAxis("Horizontal2"), 
            0.0f, Input.GetAxis("Vertical"));
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
