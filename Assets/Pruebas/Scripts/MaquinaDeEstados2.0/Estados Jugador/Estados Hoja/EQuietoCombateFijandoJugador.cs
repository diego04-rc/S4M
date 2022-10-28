using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQuietoCombateFijandoJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador
    public EQuietoCombateFijandoJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        // Si estamos andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { CambiarEstado(_fabrica.CorriendoCombateFijando()); }
        // Si solo estamos andando
        else if (_contexto.Andando)
        { CambiarEstado(_fabrica.AndandoCombateFijando()); }
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
        Debug.Log("Estado Hoja: Quieto Combate Fijado");

        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
