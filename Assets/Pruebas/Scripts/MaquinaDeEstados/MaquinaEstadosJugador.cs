using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaEstadosJugador : MonoBehaviour
{
    // ###############################################################
    // Variables Globales a los estados

    // Conjuntos de nombres de Axis para las entradas
    private readonly string[] AxisSaltar    = { "Jump" };
    private readonly string[] AxisCaminar   = { "Horizontal", "Vertical" };
    private readonly string[] AxisCorrer    = { "Run" };
    private readonly string[] AxisAtacar    = { "Fire" };

    // Minimo valor a considerar en las comprobaciones
    private const float Delta = 0.0001f;

    // Fin Variables Globales a los estados
    // ###############################################################


    void Start()
    {
        
    }

    
    void Update()
    {
        // Comprobamos si se ha saltado
        foreach (string axis in AxisSaltar)
        {
            if (Input.GetAxis(axis) > Delta)
            {
                Saltar();
                break;
            }
        }

        // Comprobamos si se esta moviendo
        foreach (string axis in AxisCaminar)
        {
            if (Input.GetAxis(axis) > Delta)
            {
                Caminar();
                break;
            }
        }

        // Comprobamos si se esta corriendo
        foreach (string axis in AxisCorrer)
        {
            if (Input.GetAxis(axis) > Delta)
            {
                Correr();
                break;
            }
        }

        // Comprobamos si ha atacado
        foreach (string axis in AxisAtacar)
        {
            if (Input.GetAxis(axis) > Delta)
            {
                Atacar();
                break;
            }
        }
    }

    // ###############################################################
    // Funciones para manejar los inputs del usuario

    void Saltar()
    {

    }

    void Correr()
    {

    }

    void Atacar()
    {

    }

    void Caminar()
    {

    }


    // Fin Funciones para manejar los inputs del usuario
    // ###############################################################
}
