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
        _contexto.VelocidadActual = _contexto.VelocidadMinCorriendo;
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento *
            _contexto.VelocidadActual;
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
        if (_contexto.VelocidadActual < _contexto.VelocidadMaxCorriendo)
        { _contexto.VelocidadActual += _contexto.IncrementoVelocidadCorriendo 
                * Time.deltaTime; }
        if (_contexto.VelocidadActual > _contexto.VelocidadMaxCorriendo)
        { _contexto.VelocidadActual = _contexto.VelocidadMaxCorriendo; }
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento *
            _contexto.VelocidadActual;
        ComprobarCambioEstado();    
    }
}
