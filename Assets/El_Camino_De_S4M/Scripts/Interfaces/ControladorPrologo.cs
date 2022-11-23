using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorPrologo : MonoBehaviour
{
    // Cantidad de escenas
    private int _numEscenas;

    // Escena actual
    private int _escenaActual;

    // Booleano para avanzar escena por evento de teclado
    private bool _avanzar;

    // Referencia al componente imagen
    [Header("Referencias a los componentes")]
    // Componente imagen donde dibujar el prologo
    [Tooltip("Imagen donde colocar el prologo")]
    [SerializeField]
    private Image _imagen;

    // Componente donde escribir el texto
    [Tooltip("Texto donde escribir")]
    [SerializeField]
    private TextMeshProUGUI _texto;

    // Componente del fondo del texto
    [Tooltip("Imagen fondo del texto")]
    [SerializeField]
    private Image _fondoTexto;

    // Alpha original del fondo del texto
    private static float AlphaOriginalFondoTexto;

    // Nombre de la siguiente escena
    [Tooltip("Nombre de la siguiente escena")]
    [SerializeField]
    private string _nombreSiguienteEscena;

    [Header("Fondos y textos")]
    // Fondos de las escenas del prologo
    [Tooltip("Fondos de las escenas del prologo")]
    [SerializeField]
    private Sprite[] _fondos;

    // Textos relacionados con los fondos
    [Tooltip("Textos relacionados con los fondos")]
    [SerializeField]
    private string[] _textos;

    [Header("Tiempos")]
    // Duracion de cada escena
    [Tooltip("Duracion de cada escena")]
    [SerializeField]
    private float[] _duracionEscenas;

    // Duracion del fade del fondo
    [Tooltip("Duracion del fade de la imagen del fondo")]
    [SerializeField]
    private float[] _fadeFondos;

    // Duracion del fade del texto
    [Tooltip("Duracion del fade del texto")]
    [SerializeField]
    private float[] _fadeTextos;

    // Duracion del fade de salida
    [Tooltip("Duracion del fade de salida")]
    [SerializeField]
    private float _fadeSalida;

    // Duracion del cambio de escena
    [Tooltip("Duracion del cambio de escena")]
    [SerializeField]
    private float _duracionCambioEscena;

    // Llamado al cargar el script
    private void Awake()
    {
        AlphaOriginalFondoTexto = _fondoTexto.color.a;
        _numEscenas = _fondos.Length;
        _escenaActual = 0;
        _avanzar = false;
    }

    // Start llamado antes del primer update
    private void Start()
    {
        // Comenzamos a reproducir el prologo
        StartCoroutine(ReproducirPrologo());
    }

    // Update is called once per frame
    void Update()
    {
        // Si se pulsa espacio, enter o escape, avanzamos la escena
        if (Input.GetKeyDown(KeyCode.Space)  || 
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Escape))
        { _avanzar = true; }
    }

    // Corutina que va reproduciendo las escenas del prologo
    IEnumerator ReproducirPrologo()
    {
        while (_escenaActual < _numEscenas)
        {
            // Inicializaciones iniciales
            float tiempoAct = 0.0f;
            Color colorFondo = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            Color colorTexto = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            Color colorFondoTexto = _fondoTexto.color;
            colorFondoTexto.a = 0.0f;

            _imagen.sprite = _fondos[_escenaActual];
            _texto.text    = _textos[_escenaActual];

            _imagen.color = colorFondo;
            _texto.color  = colorTexto;
            _fondoTexto.color = colorFondoTexto;

            // Mostramos el fondo
            while (!_avanzar && tiempoAct < _fadeFondos[_escenaActual])
            {
                float incremento = 1.0f / _fadeFondos[_escenaActual] * Time.deltaTime;
                colorFondo.r += incremento;
                colorFondo.g += incremento;
                colorFondo.b += incremento;
                _imagen.color = colorFondo;
                tiempoAct += Time.deltaTime;
                yield return null;
            }
             
            _imagen.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            _avanzar = false;
            tiempoAct = 0.0f;            

            // Mostramos el texto
            while (!_avanzar && tiempoAct < _fadeTextos[_escenaActual])
            {
                colorTexto.a += 1.0f / _fadeTextos[_escenaActual] * Time.deltaTime;
                colorFondoTexto.a += AlphaOriginalFondoTexto / _fadeTextos[_escenaActual] * Time.deltaTime;
                _texto.color = colorTexto;
                _fondoTexto.color = colorFondoTexto;
                tiempoAct += Time.deltaTime;
                yield return null;
            }

            _texto.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            _avanzar = false;
            tiempoAct = 0.0f; 

            // Esperamos la duracion de la escena
            while (!_avanzar && tiempoAct < _duracionEscenas[_escenaActual])
            {
                tiempoAct += Time.deltaTime;
                yield return null;
            }

            colorTexto.a = 1.0f;
            colorFondo.r = 1.0f;
            colorFondo.g = 1.0f;
            colorFondo.b = 1.0f;
            tiempoAct = 0.0f;

            // Salimos con un fade global
            while (!_avanzar && tiempoAct < _fadeSalida)
            {
                float incremento = 1.0f / _fadeFondos[_escenaActual] * Time.deltaTime;
                colorTexto.a -= incremento;
                colorFondoTexto.a -= incremento;
                colorFondo.r -= incremento;
                colorFondo.g -= incremento;
                colorFondo.b -= incremento;
                _imagen.color = colorFondo;
                _texto.color  = colorTexto;
                _fondoTexto.color = colorFondoTexto;
                tiempoAct += Time.deltaTime;
                yield return null;
            }

            colorTexto.a = 0.0f;
            colorFondoTexto.a = 0.0f;
            colorFondo.r = 0.0f;
            colorFondo.g = 0.0f;
            colorFondo.b = 0.0f;
            _imagen.color = colorFondo;
            _texto.color  = colorTexto;
            _fondoTexto.color = colorFondoTexto;

            // Escena siguiente
            _escenaActual += 1;
        }
        // Fin del prologo
        StartCoroutine(FinalizarPrologo());
        yield break;
    }

    // Metodo para finalizar el prologo y cambiar la escena
    IEnumerator FinalizarPrologo()
    {
        // Esperamos el tiempo de cambio de escena y cambiamos
        yield return new WaitForSeconds(_duracionCambioEscena);
        SceneManager.LoadScene(_nombreSiguienteEscena);
    }
}
