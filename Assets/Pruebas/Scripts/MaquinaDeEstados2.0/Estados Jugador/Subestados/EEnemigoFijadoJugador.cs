using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEnemigoFijadoJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador y iniciamos el estado hijo
    public EEnemigoFijadoJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) 
    { IniciarSubestado(); }

    public override void ComprobarCambioEstado()
    {
        // Si se deja de fijar al enemigo cambiamos de subestado
        if (_contexto.EnemigoFijado)
        { CambiarEstado(_fabrica.CombateLibre()); }
    }

    public override void EntrarEstado() {}

    public override void IniciarSubestado()
    {
        // Comprobamos si esta andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { AsignarSubestado(_fabrica.CorriendoCombateFijando()); }
        // Si no, si esta andando
        else if (_contexto.Andando)
        { AsignarSubestado(_fabrica.AndandoCombateFijando()); }
        // Si no, esta quieto
        else
        { AsignarSubestado(_fabrica.QuietoCombateFijando()); }
    }

    public override void SalirEstado() {}

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Subestado: Enemigo Fijado");
        // Actualzamos el estado hijo
        _subestadoActual.UpdateEstado();
        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
