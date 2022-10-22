using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorEsquivando : JugadorEstado
{
    public JugadorEsquivando(MaquinaEstadosJugador contextoActual,
    FabricaDeEstados fabricaDeEstados) : base(contextoActual, fabricaDeEstados)
    { }

    public override void ComprobarCambioEstado()
    {
        throw new System.NotImplementedException();
    }

    public override void EntrarEstado()
    {
        throw new System.NotImplementedException();
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
        ComprobarCambioEstado();
    }
}
