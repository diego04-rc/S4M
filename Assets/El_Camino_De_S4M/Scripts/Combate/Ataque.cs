using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Clase abstracta Ataque
 * 
 * Clase que representa una instancia base de un ataque. Este esta compuesto por una serie
 * de triggers que actuaran como hitbox y diran a que elementos afectara el ataque. Un ataque esta
 * compuesto por una fase previa al ataque, una espera de tiempo antes de que comienzen los efectos,
 * una duraci�n de estos efectos y una espera post-ataque seguida de unas posibles acciones que 
 * finalizan el ataque.
 */
public abstract class Ataque : MonoBehaviour
{
    // Variable para activar la visibilidad de las hitboxes
    [Tooltip("Activa la visibilidad de las hitboxes")]
    [SerializeField]
    private bool _DebugHitBoxes;

    // Variables para tratar las hitboxes
    [Tooltip("Lista con los gameobjects cuyos coliders su usar�n como triggers de la hitbox del ataque")]
    [SerializeField]
    private GameObject[] _HitBoxes = new GameObject[0];

    // Variables para tratar el comportamiento del ataque
    // Tiempo en segundos antes de que ocurra el ataque
    [Tooltip("Tiempo en segundos antes de que ocurra el ataque")]
    [SerializeField]
    private float _tiempoPrevio;

    // Tiempo en segundos que dura el ataque
    [Tooltip("Tiempo en segundos que dura el ataque")]
    [SerializeField]
    private float _duracionAtaque;

    // Tiempo en segundos despues de que ocurra el ataque
    [Tooltip("Tiempo en segundos despues de que ocurra el ataque")]
    [SerializeField]
    private float _tiempoPosterior;

    // Variables para controlar en que estado se encuentra el ataque
    public enum EstadoAtaque {PreAtaque, Ataque, PostAtaque, FinAtaque}
    private EstadoAtaque _estadoActual;
    public EstadoAtaque EstadoActual
    {
        get { return _estadoActual; }
    }
  

    public void Awake()
    {
        // Comprobamos si debug esta activo para activar o desactivar el render
        if (!_DebugHitBoxes)
        {
            foreach (GameObject hitbox in _HitBoxes)
            {
                hitbox.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    // Llamada para iniciar el ataque desde una clase externa
    public void Atacar()
    { StartCoroutine(IniciarAtaque()); }

    // Corutina que se encarga de manejar las distintas fases del ataque
    private IEnumerator IniciarAtaque()
    {
        // Establecemos el estado como preataque
        _estadoActual = EstadoAtaque.PreAtaque;

        // Ejecutamos el codigo previo al ataque y esperamos
        PrevioAtaque();
        yield return new WaitForSeconds(_tiempoPrevio);

        // Establecemos el estado como ataque
        _estadoActual = EstadoAtaque.Ataque;

        // Ejecutamos el efecto tantos frames como dure el ataque
        float tiempo = 0.0f;
        while (tiempo <= _duracionAtaque)
        {
            EfectoAtaque(CalcularObjetosEnTriggers());
            yield return null;
            tiempo += Time.deltaTime;
        }

        // Establecemos el estado como postataque
        _estadoActual = EstadoAtaque.PostAtaque;

        // Esperamos el tiempo post-ataque, ejecutamos lo necesario y finalizamos
        yield return new WaitForSeconds(_tiempoPosterior);
        PostAtaque();

        // Fin del ataque
        _estadoActual = EstadoAtaque.FinAtaque;

        yield break;
    }

    // Metodo para obtener todos los objetos que se encuentren en los triggers en un momento dado
    private List<GameObject> CalcularObjetosEnTriggers()
    {
        List<GameObject> objetos = new List<GameObject>();
        foreach (GameObject hitbox in _HitBoxes)
        {
            objetos = objetos.Union(hitbox.GetComponent<DetectorTrigger>().ObtenerGameObjects()).ToList();
        }
        return objetos;
    }

    // Metodo a ejecutar antes del ataque
    abstract protected void PrevioAtaque();

    // Metodo encargado de aplicar efectos a los objetos en los triggers
    abstract protected void EfectoAtaque(List<GameObject> objetosEnTrigger);

    // Metodo a ejecutar despues del ataque
    abstract protected void PostAtaque();
}
