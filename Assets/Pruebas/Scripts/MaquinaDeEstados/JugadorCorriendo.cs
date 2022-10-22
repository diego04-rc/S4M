using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorCorriendo : JugadorEstado
{
    public JugadorCorriendo(MaquinaEstadosJugador contextoActual,
        FabricaDeEstados fabricaDeEstados) : base(contextoActual, fabricaDeEstados)
    { }

    public override void ComprobarCambioEstado()
    {
        if (!_contexto.Andando)
        { CambiarEstado(_fabrica.Quieto()); }
        else if (!_contexto.Corriendo)
        { CambiarEstado(_fabrica.Andando()); }
    }

    public override void EntrarEstado()
    {
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento * 2.0f;
    }

    public override void IniciarSubestado()
    {

    }

    public override void SalirEstado()
    {

    }

    public override void UpdateEstado()
    {
        Debug.Log("Estado Actual Corriendo");
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento * 2.0f;
        ComprobarCambioEstado();    
    }
}
