using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAtacarCombateFijandoJugador : EstadoJugador
{
    public EAtacarCombateFijandoJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        throw new System.NotImplementedException();
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado hoja actual
        _contexto.EstadoHojaActual = MaquinaDeEstadosJugador.EstadoHoja.AtacarCombateFijando;
    }

    public override void IniciarSubestado()
    {
        throw new System.NotImplementedException();
    }

    public override void SalirEstado()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateEstado()
    {
        throw new System.NotImplementedException();
    }
}
