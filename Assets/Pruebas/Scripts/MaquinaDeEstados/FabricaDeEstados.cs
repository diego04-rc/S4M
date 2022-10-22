using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricaDeEstados
{
    private MaquinaEstadosJugador _contexto;

    public FabricaDeEstados(MaquinaEstadosJugador contextoActual)
    {
        _contexto = contextoActual;
    }

    public JugadorEstado Andando()
    {
        return new JugadorAndando(_contexto, this);
    }

    public JugadorEstado Atacando()
    {
        return new JugadorAtacando(_contexto, this);
    }

    public JugadorEstado Corriendo()
    {
        return new JugadorCorriendo(_contexto, this);
    }

    public JugadorEstado EnCombate()
    {
        return new JugadorEnCombate(_contexto, this);
    }

    public JugadorEstado EnExploracion()
    {
        return new JugadorEnExploracion(_contexto, this);
    }

    public JugadorEstado EnTierra()
    {
        return new JugadorEnTierra(_contexto, this);
    }

    public JugadorEstado Quieto()
    {
        return new JugadorQuieto(_contexto, this);
    }

    public JugadorEstado Saltando()
    {
        return new JugadorSaltando(_contexto, this);
    }
}
