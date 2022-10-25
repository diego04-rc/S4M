using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SphereCompanion : MonoBehaviour
{
    [Tooltip("Referencia a la posicion en la que se encuentra el objeto a seguir.")]
    [SerializeField]
    private Transform objetoASeguir;

    [Tooltip("Diferencia en el eje X con respecto al objeto que se esta siguiendo.")]
    [DefaultValue(0.0f)]
    [SerializeField]
    private float difX;

    [Tooltip("Diferencia en el eje Z con respecto al objeto que se esta siguiendo.")]
    [DefaultValue(0.0f)]
    [SerializeField]
    private float difZ;

    [Tooltip("Diferencia en el eje Y con respecto al objeto que se esta siguiendo.")]
    [DefaultValue(0.0f)]
    [SerializeField]
    private float difY;

    [Tooltip("Velocidad con la que se sigue al objetivo.")]
    [DefaultValue(1.0f)]
    [SerializeField]
    private float vel;

    private enum Estado {Siguiendo};
    private Estado estadoActual;
    
    void Start()
    {
        estadoActual = Estado.Siguiendo;
    }

    
    void Update()
    {
        if (estadoActual == Estado.Siguiendo)
        {
            Vector3 posObjetivo = new Vector3(objetoASeguir.position.x + difX, 
                objetoASeguir.position.y + difY, 
                objetoASeguir.position.z + difZ);

            Vector3 dir = new Vector3(posObjetivo.x - transform.position.x, 
                posObjetivo.y - transform.position.y, 
                posObjetivo.z - transform.position.z);

            if (dir.magnitude > 0.01f)
                transform.Translate(dir * Time.deltaTime * vel, Space.World);

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, objetoASeguir.forward, 2.0f * Time.deltaTime, 0.0f));
        }
    }
}
