using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEnAireCombateJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador, establecemos como nodo raiz y iniciamos el estado hijo
    public EEnAireCombateJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) 
    { _esEstadoRaiz = true; IniciarSubestado(); }

    public override void ComprobarCambioEstado()
    {
        // Comprobamos si volvemos a estar en tierra
        if (_contexto.ControladorJugador.isGrounded)
        { CambiarEstado(_fabrica.EnTierraCombate()); }
    }

    public override void EntrarEstado() 
    {
        // Establecemos el estado padre
        _contexto.EstadoPadreActual = MaquinaDeEstadosJugador.EstadoPadre.AireCombate;
    }

    public override void IniciarSubestado()
    {
        // Comprobamos si esta andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { AsignarSubestado(_fabrica.CorriendoAireCombate()); }
        // Si no, si esta andando
        else if (_contexto.Andando)
        { AsignarSubestado(_fabrica.AndandoAireCombate()); }
        // Si no, esta quieto
        else
        { AsignarSubestado(_fabrica.QuietoAireCombate()); }
    }

    public override void SalirEstado() {}

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Raiz: En Aire Combate");
        // Aplicamos la gravedad al jugador
        if (_contexto.MovY > 0.0f)
        { _contexto.MovY -= _contexto.Gravedad * Time.deltaTime; }
        // Si ademas estamos cayendo, aplicamos un extra de caida
        else
        { _contexto.MovY -= _contexto.Gravedad * _contexto.IncDeCaida * Time.deltaTime; }

        // Actualizamos el estado hijo
        _subestadoActual.UpdateEstado();
        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
