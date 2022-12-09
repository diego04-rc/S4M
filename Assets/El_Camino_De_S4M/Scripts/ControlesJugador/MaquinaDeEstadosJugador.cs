using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Camera camaraPrincipal;

    public Animator animator;

    // Variables con el estado actual
    private EstadoHoja _estadoHojaActual;
    private EstadoPadre _estadoPadreActual;
    private Subestado _subestadoActual;

    // Conjuntos de nombres de Axis para las entradas
    private readonly string[] _AxisSaltar = { "Jump" };
    private readonly string[] _AxisCaminar = { "Horizontal", "Vertical" };
    private readonly string[] _AxisCorrer = { "Run" };
    private readonly string[] _AxisAtacar = { "Fire1", "Fire2" };

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

    [Header("Componentes del jugador")]
    // Controlador del personaje jugador
    [Tooltip("Controlador del jugador")]
    [SerializeField] private CharacterController _controladorJugador;

    // Modelo del personaje
    [Tooltip("Modelo del jugador")]
    [SerializeField]
    private Transform _modeloPersonaje;

    // Trigger detector de enemigos en el area del personaje
    [Tooltip("Colider zona en la que si hay enemigos el jugador pasará a estar en combate")]
    [SerializeField]
    private DetectorTrigger _detectorEnemigos;

    [Header("Velocidades del jugador")]
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

    // Velocidad correcion direccion en combate
    [SerializeField] private float _velDirCombate;

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

    [Header("Variables del inventario")]
    // *Inicio variables control del inventario*

    // Variables para la camara del inventario
    [SerializeField] private GameObject _camaraInventario;
    [SerializeField] private GameObject _camaraNormal;
    [SerializeField] private float _coolDownCamaraInventario;
    private bool _inventarioAbierto;
    private bool _botonInventarioPulsado;
    private bool _enCoolDownInventario;

    // Menu inventario
    [SerializeField] private Canvas _menuInventario;

    // Fondos del inventario
    [SerializeField] private GameObject _fondoMochila;
    [SerializeField] private GameObject _fondoStats;
    [SerializeField] private GameObject _fondoMapas;
    [SerializeField] private GameObject _fondoMapaMundo;
    [SerializeField] private GameObject _fondoMapaPlanetas;

    // Cantidades de los items
    [SerializeField] private int _cantItemVida;
    [SerializeField] private int _cantItemVidaPlus;
    [SerializeField] private int _cantItemEnergia;
    [SerializeField] private int _cantItemEnergiaPlus;
    [SerializeField] private TextMeshProUGUI _textoItemVida;
    [SerializeField] private TextMeshProUGUI _textoItemVidaPlus;
    [SerializeField] private TextMeshProUGUI _textoItemEnergia;
    [SerializeField] private TextMeshProUGUI _textoItemEnergiaPlus;

    // *Fin Variables control del inventario*

    [Header("Esfera Acompanyante")]
    // Script que controla a la bola acompañante
    [SerializeField] private EsferaAcompanyante _acompanyante;

    [Header("Variables de la estamina y ataques")]
    // Variables para los ataques
    [SerializeField] private Ataque _ataqueLigero;
    [SerializeField] private Ataque _ataquePesado;
    private Ataque _ataqueEjecutado;
    private bool _ejecutadoAtaquePesado;
    private bool _ejecutadoAtaqueLigero;

    // *Inicio Variables para el control de la estamina*

    private float _estaminaActual;
    [SerializeField] private float _estaminaMax;
    [SerializeField] private float _costeEstaminaSalto;
    [SerializeField] private float _costeEstaminaCorrerPorSegundo;
    [SerializeField] private float _costeEstaminaAtaqueLigero;
    [SerializeField] private float _costeEstaminaAtaquePesado;
    [SerializeField] private float _costeEstaminaEsquivar;
    [SerializeField] private float _regeneracionEstaminaPorSegundo;

    // Imagen barra de estamina
    [SerializeField] private Image _estaminaUI;

    // *Fin variables control de la estamina*

    // *Variables de vida*

    [Header("Variables para la vida")]

    // Trozos de la vida del personaje
    [SerializeField] private GameObject[] _barraVida;

    // Caras de S4M
    [SerializeField] private GameObject[] _carasS4M;

    // Tiempo que dura la cara de danyo
    [SerializeField] private int _tiempoDanyo;

    // Invulnerabilidad
    private bool _invulnerable;

    // Variables para controlar la vida actual
    private int _vida;

    // Vida maxima
    private int _vidaMaxima;

    // *Fin variables de vida*

    // Variable para saber si el juego esta en pausa
    private bool _pausado;

    [Header("Menu de pausa")]
    // Gameobject con el menu de pausa del HUD
    [Tooltip("Gameobject con el menu de pausa")]
    [SerializeField]
    private GameObject _menuPausa;

    // Gameobject con el menu de opciones
    [Tooltip("Gameobject con el menu de opciones")]
    [SerializeField]
    private GameObject _menuOpciones;

    // Fin Variables Globales
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

        // Camara del inventario desactivada
        _camaraInventario.SetActive(false);

        // Camara normal siempre activa
        _camaraNormal.SetActive(true);

        // No se ha abierto el inventario y el cooldown esta desactivado
        _inventarioAbierto = false;
        _enCoolDownInventario = false;

        // Menu de inventario desactivado
        _menuInventario.enabled = false;

        // Fondo inventario inicial a la mochila
        _fondoMochila.SetActive(true);

        // Actualizamos los items
        actualizarCantidadesItems();

        // Desactivamos el resto de fondos
        _fondoStats.SetActive(false);
        _fondoMapas.SetActive(false);

        // Comenzamos con la estamina maxima
        _estaminaActual = _estaminaMax;

        // Iniciamos la continua recuperacion de estamina
        StartCoroutine(RecuperacionDeEstamina());

        // Vida maxima vale 4
        _vidaMaxima = 4;

        // La vida inicial igual a la maxima
        _vida = _vidaMaxima;

        // No es invulnerable
        _invulnerable = false;

        // El inventario usara la camara principal
        _menuInventario.worldCamera = Camera.main;

        // El juego inicialmente no esta pausado
        _pausado = false;
    }

    void Update()
    {

        if (EstadoHojaActual == EstadoHoja.Andando || EstadoHojaActual == EstadoHoja.AndandoCombate
            || EstadoHojaActual == EstadoHoja.AndandoCombateFijando)
            animator.SetBool("Andando", true);
        else
            animator.SetBool("Andando", false);

        if (EstadoHojaActual == EstadoHoja.Corriendo || EstadoHojaActual == EstadoHoja.CorriendoCombate
            || EstadoHojaActual == EstadoHoja.CorriendoCombateFijando)
            animator.SetBool("Corriendo", true);
        else
            animator.SetBool("Corriendo", false);

        if (EstadoPadreActual == EstadoPadre.InteractuandoConEntorno)
        {
            animator.SetBool("Andando", false);
            animator.SetBool("Corriendo", false);
        }

        if (EstadoPadreActual == EstadoPadre.TierraCombate)
        {
            if (EstadoHojaActual == EstadoHoja.AtacarCombate)
            {
                animator.SetLayerWeight(2, 1.0f);
                animator.SetBool("Atacando", false);
                animator.SetLayerWeight(1, 0.0f);
                animator.SetBool("EnCombate", false);
            }
            else
            {
                animator.SetLayerWeight(2, 0.0f);
                animator.SetBool("Atacando", true);
                animator.SetLayerWeight(1, 0.6f);
                animator.SetBool("EnCombate", false);
            }
            
        }
        else
        {
            animator.SetLayerWeight(1, 0.0f);
            animator.SetBool("EnCombate", true);
            animator.SetLayerWeight(2, 0.0f);
            animator.SetBool("Atacando", true);
        }

        // Comprobamos si se ha pausado o despausado el juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pausado)
            {
                _menuPausa.SetActive(false);
                Time.timeScale = 1.0f;
                _pausado = false;
            }
            else
            {
                _menuPausa.SetActive(true);
                _pausado = true;
                Time.timeScale = 0.0f;
                return;
            }
        }

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

        // Comprobamos si se ha abierto el inventario
        _inventarioAbierto = false;
        if (Input.GetKeyUp(KeyCode.Q) || _botonInventarioPulsado)
        {
            _inventarioAbierto = true;
            _botonInventarioPulsado = false;
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

    // Metodo para iniciar el cooldown del inventario
    public void IniciarCoolDownInventario()
    {
        StartCoroutine(IniciarCoolDownInventarioCorutina());
    }

    // Metodo para reducir la estamina actual: devuelve true si se puede realizar
    // la accion, y false en caso de que no
    public bool ReducirEstamina(float reduccion)
    {
        if (_estaminaActual < reduccion)
        { return false; }
        else
        {
            _estaminaActual -= reduccion;
            _estaminaUI.fillAmount = _estaminaActual / _estaminaMax;
            return true;
        }
    }

    // Corutina para iniciar el cooldown del esquive
    private IEnumerator IniciarCoolDownCorutina()
    {
        _enCoolDownEsquive = true;
        yield return new WaitForSeconds(_coolDownEsquive);
        _enCoolDownEsquive = false;
        yield break;
    }

    // Corutina para iniciar el cooldown del inventario
    private IEnumerator IniciarCoolDownInventarioCorutina()
    {
        _enCoolDownInventario = true;
        yield return new WaitForSeconds(_coolDownCamaraInventario);
        _enCoolDownInventario = false;
        yield break;
    }

    // Corutina para recuperar la estamina
    private IEnumerator RecuperacionDeEstamina()
    {
        while (true)
        {
            yield return null;
            _estaminaActual += _regeneracionEstaminaPorSegundo * Time.deltaTime;
            if (_estaminaActual > _estaminaMax)
            { _estaminaActual = _estaminaMax; }
            _estaminaUI.fillAmount = _estaminaActual / _estaminaMax;
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
        Vector3 delante = camaraPrincipal.transform.forward * Input.GetAxis("Vertical");
        Vector3 derecha = camaraPrincipal.transform.right * Input.GetAxis("Horizontal");
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
        if (Input.GetAxis("Fire1") != 0.0)
        { 
            _ejecutadoAtaqueLigero = true;
            _ejecutadoAtaquePesado = false;
        }
        else
        {
            _ejecutadoAtaquePesado = true;
            _ejecutadoAtaqueLigero = false;
        }
    }

    // Fin metodos para las acciones de los inputs
    //##############################################################

    //##############################################################
    // Inicio metodos para el inventario

    public void CerrarInventario()
    { _botonInventarioPulsado = true; }

    public void BotonInventarioMochila()
    {
        _fondoMochila.SetActive(true);
        _fondoStats.SetActive(false);
        _fondoMapas.SetActive(false);
        actualizarCantidadesItems();
    }

    private void actualizarCantidadesItems()
    {
        _textoItemVida.text = _cantItemVida.ToString();
        _textoItemVidaPlus.text = _cantItemVidaPlus.ToString();
        _textoItemEnergia.text = _cantItemEnergia.ToString();
        _textoItemEnergiaPlus.text = _cantItemEnergiaPlus.ToString();
    }

    public void BotonInventarioStats()
    {
        _fondoMochila.SetActive(false);
        _fondoStats.SetActive(true);
        _fondoMapas.SetActive(false);
    }

    public void BotonInventarioMapaMundo()
    {
        _fondoMochila.SetActive(false);
        _fondoStats.SetActive(false);
        _fondoMapas.SetActive(true);
    }

    public void BotonInventarioCambioMapa()
    {
        _fondoMapaMundo.SetActive(!_fondoMapaMundo.activeSelf);
        _fondoMapaPlanetas.SetActive(!_fondoMapaPlanetas.activeSelf);
    }

    public void anyadirItemVida(int cantidad)
    {
        _cantItemVida += cantidad;
    }

    public void anyadirItemVidaPlus(int cantidad)
    {
        _cantItemVidaPlus += cantidad;
    }

    public void anyadirItemEnergia(int cantidad)
    {
        _cantItemEnergia += cantidad;
    }

    public void anyadirItemEnergiaPlus(int cantidad)
    {
        _cantItemEnergiaPlus += cantidad;
    }

    // Fin metodos para el inventario
    //##############################################################

    //##############################################################
    // Inicio metodos para la gestion de la vida

    public IEnumerator CaraRecibirDanyo()
    {
        _carasS4M[0].SetActive(false);
        _carasS4M[1].SetActive(true);
        _invulnerable = true;
        yield return new WaitForSeconds(_tiempoDanyo);
        _carasS4M[1].SetActive(false);
        _carasS4M[0].SetActive(true);
        _invulnerable = false;
    }

    public void RecibirDanyo()
    {
        if (!_invulnerable)
        {
            if (_vida == 1)
            {
                Muerte();
            }
            else
            {
                _barraVida[_vida - 1].SetActive(false);
                _vida--;
                _barraVida[_vida - 1].SetActive(true);
                StartCoroutine(CaraRecibirDanyo());
            }
        }
    }

    public void UsarItemVida()
    {
        if (_vida < _vidaMaxima && _cantItemVida > 0)
        {
            _vida++;
            _cantItemVida--;
        }
    }

    public void UsarItemVidaPlus()
    {
        if (_vida < _vidaMaxima && _cantItemVidaPlus > 0)
        {
            _vida += 2;
            _cantItemVidaPlus--;
            if (_vida > _vidaMaxima)
            {
                _vida = _vidaMaxima;
            }
        }
    }

    private void Muerte()
    {

    }

    // Fin metodos para la gestion de la vida
    //##############################################################

    //##############################################################
    // Inicio metodos del menu de pausa

    public void BotonContinuarPulsado()
    {
        _menuPausa.SetActive(false);
        Time.timeScale = 1.0f;
        _pausado = false;
    }

    public void BotonOpcionesPulsado()
    {
        _menuOpciones.SetActive(true);
    }

    public void BotonVolverPulsado()
    {
        _menuOpciones.SetActive(false);
    }

    public void BotonSalirPulsado()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Fin metodos del menu de pausa
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
    public float VelDirCombate
    {
        get { return _velDirCombate; }
        set { _velDirCombate = value; }
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
    public GameObject CamaraInventario
    {
        get { return _camaraInventario; }
        set { _camaraInventario = value; }
    }
    public float CoolDownCamaraInventario
    {
        get { return _coolDownCamaraInventario; }
        set { _coolDownCamaraInventario = value; }
    }
    public bool InventarioAbierto
    {
        get { return _inventarioAbierto; }
        set { _inventarioAbierto = value; }
    }
    public bool EnCoolDownInventario
    {
        get { return _enCoolDownInventario; }
        set { _enCoolDownInventario = value; }
    }

    public Canvas MenuInventario
    { 
        get { return _menuInventario; }
        set { _menuInventario = value; }
    }

    public EsferaAcompanyante Acompanyante
    {
        get { return _acompanyante; }
        set { _acompanyante = value; }
    }
    public Ataque AtaqueLigero
    {
        get { return _ataqueLigero; }
        set { _ataqueLigero = value; }
    }
    public Ataque AtaquePesado
    {
        get { return _ataquePesado; }
        set { _ataquePesado = value; }
    }
    public Ataque AtaqueEjecutado
    {
        get { return _ataqueEjecutado; }
        set { _ataqueEjecutado = value; }
    }
    public bool EjecutadoAtaqueLigero
    {
        get { return _ejecutadoAtaqueLigero; }
        set { _ejecutadoAtaqueLigero = value; }
    }
    public bool EjecutadoAtaquePesado
    {
        get { return _ejecutadoAtaquePesado; }
        set { _ejecutadoAtaquePesado = value; }
    }
    public float EstaminaActual
    {
        get { return _estaminaActual; }
        set { _estaminaActual = value; }
    }
    public float EstaminaMax
    {
        get { return _estaminaMax; }
        set { _estaminaMax = value; }
    }
    public float CosteEstaminaSalto
    {
        get { return _costeEstaminaSalto; }
        set { _costeEstaminaSalto = value; }
    }
    public float CosteEstaminaCorrerPorSegundo
    {
        get { return _costeEstaminaCorrerPorSegundo; }
        set { _costeEstaminaCorrerPorSegundo = value; }
    }
    public float CosteEstaminaAtaqueLigero
    {
        get { return _costeEstaminaAtaqueLigero; }
        set { _costeEstaminaAtaqueLigero = value; }
    }
    public float CosteEstaminaAtaquePesado
    {
        get { return _costeEstaminaAtaquePesado; }
        set { _costeEstaminaAtaquePesado = value; }
    }
    public float CosteEstaminaEsquivar
    {
        get { return _costeEstaminaEsquivar; }
        set { _costeEstaminaEsquivar = value; }
    }
    public float RegeneracionEstaminaPorSegundo
    {
        get { return _regeneracionEstaminaPorSegundo; }
        set { _regeneracionEstaminaPorSegundo = value; }
    }
    public Image EstaminaUI
    {
        get { return _estaminaUI; }
        set { _estaminaUI = value; }
    }

    // Fin Getters y Setters
    //##############################################################
}