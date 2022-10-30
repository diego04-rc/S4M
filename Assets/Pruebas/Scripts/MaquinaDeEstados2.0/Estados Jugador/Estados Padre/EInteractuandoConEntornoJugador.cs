using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EInteractuandoConEntornoJugador : EstadoJugador
{
    public EInteractuandoConEntornoJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        throw new System.NotImplementedException();
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado padre
        _contexto.EstadoPadreActual = MaquinaDeEstadosJugador.EstadoPadre.InteractuandoConEntorno;
    }

    public override void IniciarSubestado()
    {
        throw new System.NotImplementedException();
    }

    public override void SalirEstado()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateEstado()
    {
        throw new System.NotImplementedException();
    }
}
