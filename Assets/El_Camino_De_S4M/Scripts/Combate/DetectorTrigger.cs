using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Clase DetectorTrigger
 * 
 * Aplicada a un gameobject con un colider y un rigidbody, se encarga de mantener una lista
 * con todos aquellos objetos pertencientes a los tags indicados que entren en el rango de
 * dicho colider. De esta forma, actua como un trigger que almacena los objetos que se encuentran
 * dentro del mismo.
 */
public class DetectorTrigger : MonoBehaviour
{
    // Variable para activar la muestra de los objetos que se encuentran en el trigger
    [Tooltip("Mostrar por consola los objetos que se encuentran en el trigger")]
    [SerializeField]
    private bool _DebugGameObjects;

    // Lista de los objetos que se encuentran en un momento dado en el trigger
    private List<GameObject> _objetosEnTrigger;

    // Tags de los objetos que pueden ser detectados por el trigger
    [SerializeField]
    private string[] _tagsAceptados;

    void Awake()
    {
        // Inicializamos la lista y nos aseguramos de que el colider sea un trigger
        _objetosEnTrigger = new List<GameObject>();
        gameObject.GetComponent<Collider>().isTrigger = true;
    }

    private void Update()
    {
        // Comprobamos si hay objetos destruidos
        int enemigos = 0;
        foreach (GameObject g in _objetosEnTrigger.ToList())
        {
            if (g == null) { _objetosEnTrigger.Remove(g); }
        }

        // En caso de estar activo el debug, mostramos los objetos que se encuentran en el trigger
        if (_DebugGameObjects)
        {
            Debug.Log("Cantidad de objetos: " + _objetosEnTrigger.Count);
            for (var i = 0; i < _objetosEnTrigger.Count; i++)
            {
                Debug.Log(i + ":U+0020" + _objetosEnTrigger[i].name);
            }
        } 
    }

    // Metodo para detectar entradas de objetos en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si tiene un tag aceptado se añade a la lista
        if (_tagsAceptados.Contains(other.tag))
        {
            _objetosEnTrigger.Add(other.gameObject);
        }
    }

    // Metodo para detectar salidas de objetos en el trigger
    private void OnTriggerExit(Collider other)
    {
        // Se elimina de la lista el objeto en cuestion
        _objetosEnTrigger.Remove(other.gameObject);
    }

    // Metodo para obtener la lista de objetos en un momento dado
    public List<GameObject> ObtenerGameObjects()
    {
        return _objetosEnTrigger;
    }
}
