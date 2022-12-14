using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQuietoAireJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador
    public EQuietoAireJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        // Si estamos andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { CambiarEstado(_fabrica.CorriendoAire()); }
        // Si solo estamos andando
        else if (_contexto.Andando)
        { CambiarEstado(_fabrica.AndandoAire()); }
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado hoja actual
        _contexto.EstadoHojaActual = MaquinaDeEstadosJugador.EstadoHoja.QuietAire;

        // Nos aseguramos de que el movimiento sea cero
        _contexto.MovFinal = Vector3.zero;
    }

    public override void IniciarSubestado() { }

    public override void SalirEstado() { }

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Hoja: Quieto Aire");

        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
