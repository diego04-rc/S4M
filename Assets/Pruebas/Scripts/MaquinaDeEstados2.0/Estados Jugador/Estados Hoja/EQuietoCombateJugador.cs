using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQuietoCombateJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador
    public EQuietoCombateJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        // Si estamos andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { CambiarEstado(_fabrica.CorriendoCombate()); }
        // Si solo estamos andando
        else if (_contexto.Andando)
        { CambiarEstado(_fabrica.AndandoCombate()); }
    }

    public override void EntrarEstado()
    {
        // Nos aseguramos de que el movimiento sea cero
        _contexto.MovFinal = Vector3.zero;
    }

    public override void IniciarSubestado() { }

    public override void SalirEstado() { }

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Hoja: Quieto Combate");

        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
