using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECombateLibreJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador y iniciamos el estado hijo
    public ECombateLibreJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) 
    { IniciarSubestado(); }

    public override void ComprobarCambioEstado()
    {
        // Si se fija a algun enemigo cambiamos de subestado
        if (_contexto.EnemigoFijado)
        { CambiarEstado(_fabrica.EnemigoFijado()); }
    }

    public override void EntrarEstado() 
    {
        // Establemcemos el subestado actual
        _contexto.SubestadoActual = MaquinaDeEstadosJugador.Subestado.CombateLibre;
    }

    public override void IniciarSubestado()
    {
        // Comprobamos si esta andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { AsignarSubestado(_fabrica.CorriendoCombate()); }
        // Si no, si esta andando
        else if (_contexto.Andando)
        { AsignarSubestado(_fabrica.AndandoCombate()); }
        // Si no quiza ha atacado
        else if (_contexto.EjecutadoAtaqueLigero || _contexto.EjecutadoAtaquePesado)
        { AsignarSubestado(_fabrica.AtacarCombate()); }
        // Si no, esta quieto
        else
        { AsignarSubestado(_fabrica.QuietoCombate()); }
    }

    public override void SalirEstado() 
    {
        // Ponemos el estado en vacio
        _contexto.SubestadoActual = MaquinaDeEstadosJugador.Subestado.EstadoVacio;
    }

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Subestado: Combate Libre");
        // Actualzamos el estado hijo
        _subestadoActual.UpdateEstado();
        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
