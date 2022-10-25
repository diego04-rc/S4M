using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorEnTierra : JugadorEstado
{
    public JugadorEnTierra(MaquinaEstadosJugador contextoActual,
           FabricaDeEstados fabricaDeEstados) : base(contextoActual, fabricaDeEstados)
    {
        _esEstadoRaiz = true;
        IniciarSubestado();
    }

    public override void ComprobarCambioEstado()
    {
        Debug.Log(_contexto.ControladorJugador.isGrounded);
        if (_subestadoActual.GetType() != typeof(JugadorEnCombate))
        {
            if (_contexto.Saltando || !_contexto.ControladorJugador.isGrounded)
            {
                CambiarEstado(_fabrica.Saltando());
            }
        }
    }

    public override void EntrarEstado()
    {
        _contexto.MovimientoY -= 5.0f;
    }

    public override void IniciarSubestado()
    {
        AsignarSubestado(_fabrica.EnCombate());
    }

    public override void SalirEstado()
    {

    }

    public override void UpdateEstado()
    {
        Debug.Log("Estado Actual En Tierra");
        _subestadoActual.UpdateEstado();
        ComprobarCambioEstado();
    }
}
