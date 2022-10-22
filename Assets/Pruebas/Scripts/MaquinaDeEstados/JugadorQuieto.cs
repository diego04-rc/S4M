using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorQuieto : JugadorEstado
{
    public JugadorQuieto(MaquinaEstadosJugador contextoActual,
           FabricaDeEstados fabricaDeEstados) : base(contextoActual, fabricaDeEstados)
    { }

    public override void ComprobarCambioEstado()
    {
        if (_contexto.Andando && _contexto.Corriendo)
        { CambiarEstado(_fabrica.Corriendo()); }
        else if (_contexto.Andando)
        { CambiarEstado(_fabrica.Andando()); }
    }

    public override void EntrarEstado()
    {
        _contexto.MovimientoAplicado = Vector3.zero;
    }

    public override void IniciarSubestado()
    {

    }

    public override void SalirEstado()
    {

    }

    public override void UpdateEstado()
    {
        Debug.Log("Estado Actual Quieto");
        ComprobarCambioEstado();
    }
}
