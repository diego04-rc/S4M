using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorEnCombate : JugadorEstado
{
    public JugadorEnCombate(MaquinaEstadosJugador contextoActual,
        FabricaDeEstados fabricaDeEstados) : base(contextoActual, fabricaDeEstados)
    {
        IniciarSubestado();
    }

    public override void ComprobarCambioEstado()
    {
        
    }

    public override void EntrarEstado()
    {
        
    }

    public override void IniciarSubestado()
    {
        if (!_contexto.Andando)
        { AsignarSubestado(_fabrica.Quieto()); }
        else if (_contexto.Andando && !_contexto.Corriendo)
        { AsignarSubestado(_fabrica.Andando()); }
        else
        { AsignarSubestado(_fabrica.Corriendo()); }
    }

    public override void SalirEstado()
    {

    }

    public override void UpdateEstado()
    {
        Debug.Log("Estado Actual En Combate");
        _subestadoActual.UpdateEstado();
        ComprobarCambioEstado();
    }
}
