using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorAndando : JugadorEstado
{
    public JugadorAndando(MaquinaEstadosJugador contextoActual,
        FabricaDeEstados fabricaDeEstados) : base(contextoActual, fabricaDeEstados)
    { }

    public override void ComprobarCambioEstado()
    {
        if (!_contexto.Andando)
        { CambiarEstado(_fabrica.Quieto()); }
        else if (_contexto.Corriendo)
        { CambiarEstado(_fabrica.Corriendo()); }
    }

    public override void EntrarEstado()
    {
        _contexto.VelocidadActual = _contexto.VelocidadMinAndando;
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento *
            _contexto.VelocidadActual;
    }

    public override void IniciarSubestado()
    {
;
    }

    public override void SalirEstado()
    {

    }

    public override void UpdateEstado()
    {
        Debug.Log("Estado Actual Andando");
        if (_contexto.VelocidadActual < _contexto.VelocidadMaxAndando)
        { _contexto.VelocidadActual += _contexto.IncrementoVelocidadAndando 
                * Time.deltaTime; }
        if (_contexto.VelocidadActual > _contexto.VelocidadMaxAndando)
        { _contexto.VelocidadActual = _contexto.VelocidadMaxAndando; }
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento * 
            _contexto.VelocidadActual;
        Vector3 targetDir = _contexto.MovimientoAplicado;
        Vector3 newDir = Vector3.RotateTowards(_contexto.Cuerpo.forward, targetDir, 2 * Time.deltaTime, 0.0f);
        newDir.y = 0.0f;
        _contexto.Cuerpo.rotation = Quaternion.LookRotation(newDir);
        ComprobarCambioEstado(); 
    }
}
