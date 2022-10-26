using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricaDeEstadosJugador
{
    private MaquinaDeEstadosJugador _contexto;

    public FabricaDeEstadosJugador(MaquinaDeEstadosJugador contextoActual)
    { _contexto = contextoActual; }
    
    public EstadoJugador EnAireCombate()
    { return new EEnAireCombateJugador(_contexto, this); }

    public EstadoJugador EnAireExploracion()
    { return new EEnAireExploracionJugador(_contexto, this); }

    public EstadoJugador EnTierraCombate()
    { return new EEnTierraCombateJugador(_contexto, this); }

    public EstadoJugador EnTierraExploracion()
    { return new EEnTierraExploracionJugador(_contexto, this); }

    public EstadoJugador InteractuandoConEntorno()
    { return new EInteractuandoConEntornoJugador(_contexto, this); }

    public EstadoJugador CombateLibre()
    { return new ECombateLibreJugador(_contexto, this); }

    public EstadoJugador EnemigoFijado()
    { return new EEnemigoFijadoJugador(_contexto, this); }

    public EstadoJugador AndandoAireCombate()
    { return new EAndandoAireCombateJugador(_contexto, this); }

    public EstadoJugador AndandoAire()
    { return new EAndandoAireJugador(_contexto, this); }

    public EstadoJugador AndandoCombateFijado()
    { return new EAndandoCombateFijandoJugador(_contexto, this); }

    public EstadoJugador AndandoCombate()
    { return new EAndandoCombateJugador(_contexto, this); }

    public EstadoJugador Andando()
    { return new EAndandoJugador(_contexto, this); }

    public EstadoJugador AtacarCombateFijado()
    { return new EAtacarCombateFijandoJugador(_contexto, this); }

    public EstadoJugador AtacarCombate()
    { return new EAtacarCombateJugador(_contexto, this); }

    public EstadoJugador CorriendoAireCombate()
    { return new ECorriendoAireCombateJugador(_contexto, this); }

    public EstadoJugador CorriendoAire()
    { return new ECorriendoAireJugador(_contexto, this); }

    public EstadoJugador CorriendoCombateFijado()
    { return new ECorriendoCombateFijandoJugador(_contexto, this); }

    public EstadoJugador CorriendoCombate()
    { return new ECorriendoCombateJugador(_contexto, this); }

    public EstadoJugador Corriendo()
    { return new ECorriendoJugador(_contexto, this); }

    public EstadoJugador EsquivarCombateFijado()
    { return new EEsquivarCombateFijandoJugador(_contexto, this); }

    public EstadoJugador EsquivarCombate()
    { return new EEsquivarCombateJugador(_contexto, this); }

    public EstadoJugador QuietoAireCombate()
    { return new EQuietoAireCombateJugador(_contexto, this); }

    public EstadoJugador QuietoAire()
    { return new EQuietoAireJugador(_contexto, this); }

    public EstadoJugador QuietoCombateFijando()
    { return new EQuietoCombateFijandoJugador(_contexto, this); }

    public EstadoJugador QuietoCombate()
    { return new EQuietoCombateJugador(_contexto, this); }

    public EstadoJugador Quieto()
    { return new EQuietoJugador(_contexto, this); }
}
