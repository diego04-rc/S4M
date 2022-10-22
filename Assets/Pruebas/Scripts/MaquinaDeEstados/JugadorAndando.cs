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
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento;
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
        _contexto.MovimientoAplicado = _contexto.VectorMovimiento;
        ComprobarCambioEstado(); 
    }
}
