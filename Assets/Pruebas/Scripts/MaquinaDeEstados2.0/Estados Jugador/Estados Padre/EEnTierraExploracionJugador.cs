using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEnTierraExploracionJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador, establecemos como nodo raiz y iniciamos el estado hijo
    public EEnTierraExploracionJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) 
    { _esEstadoRaiz = true; IniciarSubestado(); }

    public override void ComprobarCambioEstado()
    {
        // Comprobamos si el jugador pasa a estar en el aire

        // Si no, comprobamos si pasa a estar en combate

        // Si no, comprobamos si esta interactuando con el entorno
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
        throw new System.NotImplementedException();
    }
}
