using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAtacarCombateJugador : EstadoJugador
{
    public EAtacarCombateJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        if (_contexto.AtaqueLigero.EstadoActual == Ataque.EstadoAtaque.FinAtaque)
        {
            // Comprobamos si esta andando y corriendo
            if (_contexto.Andando && _contexto.Corriendo)
            {
                if (_contexto.ReducirEstamina(_contexto.CosteEstaminaCorrerPorSegundo * Time.deltaTime))
                { CambiarEstado(_fabrica.CorriendoCombate()); }
            }
            // Si no, si esta andando
            else if (_contexto.Andando)
            { CambiarEstado(_fabrica.AndandoCombate()); }
            // Si no, esta quieto
            else
            { CambiarEstado(_fabrica.QuietoCombate()); }
        }
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado hoja actual
        _contexto.EstadoHojaActual = MaquinaDeEstadosJugador.EstadoHoja.AtacarCombate;

        // Si se ha ejecutado un ligero
        if (_contexto.EjecutadoAtaqueLigero)
        { _contexto.AtaqueEjecutado = _contexto.AtaqueLigero; }
        // Si se ha ejecutado un pesado
        else if (_contexto.EjecutadoAtaquePesado)
        { _contexto.AtaqueEjecutado = _contexto.AtaquePesado; }

        // Iniciamos el ataque
        _contexto.AtaqueEjecutado.Atacar();

        // Dejamo de movernos
        _contexto.MovFinal = Vector3.zero;
    }

    public override void IniciarSubestado() {}

    public override void SalirEstado() 
    {
        // Dejamos de atacar
        _contexto.EjecutadoAtaqueLigero = false;
        _contexto.EjecutadoAtaquePesado = false;
    }

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado hoja: Atacando");
        // Comprobamos si ha acabado el ataque
        ComprobarCambioEstado();
    }
}
