using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorSaltando : JugadorEstado
{
    public JugadorSaltando(MaquinaEstadosJugador contextoActual,
           FabricaDeEstados fabricaDeEstados) : base(contextoActual, fabricaDeEstados)
    {
        _esEstadoRaiz = true;
        IniciarSubestado();
    }

    public override void ComprobarCambioEstado()
    {
        if (_contexto.ControladorJugador.isGrounded)
        {
            CambiarEstado(_fabrica.EnTierra());
        }
    }

    public override void EntrarEstado()
    {
        Debug.Log("Saltando");
        if (_contexto.Saltando)
            _contexto.MovimientoY = _contexto.VelocidadSalto;
    }

    public override void IniciarSubestado()
    {
        AsignarSubestado(_fabrica.EnExploracion());
    }

    public override void SalirEstado()
    {
        
    }

    public override void UpdateEstado()
    {
        Debug.Log("Estado Actual Saltando");
        if (_contexto.MovimientoY < 0.0f)
        {
            _contexto.MovimientoY += Physics.gravity.y * 
                _contexto.VelocidadExtraCaida * Time.deltaTime;
        }
        else
        {
            _contexto.MovimientoY += Physics.gravity.y * Time.deltaTime;
        }
        
        _subestadoActual.UpdateEstado();
        ComprobarCambioEstado();
    }
}
