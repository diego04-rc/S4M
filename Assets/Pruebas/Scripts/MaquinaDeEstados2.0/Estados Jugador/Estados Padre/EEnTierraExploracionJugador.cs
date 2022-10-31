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
        // Comprobamos si el jugador pasa a estar en el aire (salto o caida)
        if (_contexto.Saltado || !_contexto.ControladorJugador.isGrounded)
        { CambiarEstado(_fabrica.EnAireExploracion()); }
        // Si no, comprobamos si pasa a estar en combate
        else if (_contexto.EnemigosCerca())
        { CambiarEstado(_fabrica.EnTierraCombate()); }
        // Si no, comprobamos si esta interactuando con el entorno
        // Abriendo Inventario
        else if (_contexto.InventarioAbierto && !_contexto.EnCoolDownInventario)
        { CambiarEstado(_fabrica.InteractuandoConEntorno()); }
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado padre
        _contexto.EstadoPadreActual = MaquinaDeEstadosJugador.EstadoPadre.TierraExploracion;

        // Nos aseguramos de que se detecta que el jugador esta en el suelo 
        // añadiendo algo de velocidad en Y
        _contexto.MovY = -10.0f;
    }

    public override void IniciarSubestado()
    {
        // Comprobamos si esta andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { AsignarSubestado(_fabrica.Corriendo()); }
        // Si no, si esta andando
        else if (_contexto.Andando)
        { AsignarSubestado(_fabrica.Andando()); }
        // Si no, esta quieto
        else
        { AsignarSubestado(_fabrica.Quieto()); }
    }

    public override void SalirEstado() {}

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Raiz: En Tierra Exploracion");
        // Actualzamos el estado hijo
        _subestadoActual.UpdateEstado();
        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
