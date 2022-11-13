using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EsferaAcompanyante : MonoBehaviour
{
    [Tooltip("Referencia a la posicion en la que se encuentra el objeto a seguir.")]
    [SerializeField]
    private Transform _objetoASeguir;

    [Tooltip("Referencia a la posicion cuando se abre el inventario.")]
    [SerializeField]
    private Transform _inventario;

    [Tooltip("Diferencia en el eje X con respecto al objeto que se esta siguiendo.")]
    [DefaultValue(0.0f)]
    [SerializeField]
    private float _difX;

    [Tooltip("Diferencia en el eje Z con respecto al objeto que se esta siguiendo.")]
    [DefaultValue(0.0f)]
    [SerializeField]
    private float _difZ;

    [Tooltip("Diferencia en el eje Y con respecto al objeto que se esta siguiendo.")]
    [DefaultValue(0.0f)]
    [SerializeField]
    private float _difY;

    [Tooltip("Velocidad con la que se sigue al objetivo.")]
    [DefaultValue(1.0f)]
    [SerializeField]
    private float _vel;

    public enum EstadoAcompanyante { Siguiendo, Inventario, MostrandoInventario };
    private EstadoAcompanyante _estadoActual;

    public EstadoAcompanyante EstadoActual { get { return _estadoActual; } }

    void Start()
    {
        _estadoActual = EstadoAcompanyante.Siguiendo;
    }
    
    void Update()
    {
        if (_estadoActual == EstadoAcompanyante.Siguiendo)
        {
            Vector3 posObjetivo = new Vector3(_objetoASeguir.position.x + _difX,
                _objetoASeguir.position.y + _difY,
                _objetoASeguir.position.z + _difZ);

            Vector3 dir = new Vector3(posObjetivo.x - transform.position.x,
                posObjetivo.y - transform.position.y,
                posObjetivo.z - transform.position.z);

            if (dir.magnitude > 0.01f)
                transform.Translate(dir * Time.deltaTime * _vel, Space.World);

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _objetoASeguir.forward, 2.0f * Time.deltaTime, 0.0f));
        }
        else if (_estadoActual == EstadoAcompanyante.Inventario)
        {
            Vector3 posObjetivo = new Vector3(_inventario.position.x,
                _inventario.position.y,
                _inventario.position.z);

            Vector3 dir = new Vector3(posObjetivo.x - transform.position.x,
                posObjetivo.y - transform.position.y,
                posObjetivo.z - transform.position.z);

            if (dir.magnitude > 0.1f)
                transform.Translate(dir * Time.deltaTime * _vel, Space.World);
            else
                _estadoActual = EstadoAcompanyante.MostrandoInventario;
                

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, _inventario.forward, 5.0f * Time.deltaTime, 0.0f));
        }
    }

    public void cambiarEstado(EstadoAcompanyante nuevoEstado)
    {
        _estadoActual = nuevoEstado;
    }
}
