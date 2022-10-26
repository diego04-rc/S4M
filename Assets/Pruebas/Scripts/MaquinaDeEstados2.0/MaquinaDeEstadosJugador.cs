using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaDeEstadosJugador : MonoBehaviour
{
    //##############################################################
    // Inicio Variables Globales

    // Variables para controlar el estado del jugador
    private EstadoJugador _estadoActual;
    private FabricaDeEstadosJugador _fabricaEstados;

    // Fin Variables Globales
    //##############################################################

    //##############################################################
    // Inicio Getters y Setters

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

    // Fin Getters y Setters
    //##############################################################

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
