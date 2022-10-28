using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EEnTierraCombateJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador, establecemos como nodo raiz y iniciamos el estado hijo
    public EEnTierraCombateJugador(MaquinaDeEstadosJugador contextoActual,
         FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) 
    { _esEstadoRaiz = true; IniciarSubestado(); }

    public override void ComprobarCambioEstado()
    {
        // Comprobamos si el jugador pasa a estar en el aire (caida)
        if (!_contexto.ControladorJugador.isGrounded)
        { CambiarEstado(_fabrica.EnAireCombate()); }
        // Si no comprobamos si dejamos de estar en combate
        else if (!_contexto.EnemigosCerca())
        { CambiarEstado(_fabrica.EnTierraExploracion()); }
    }

    public override void EntrarEstado()
    {
        // Nos aseguramos de que se detecta que el jugador esta en el suelo 
        // añadiendo algo de velocidad en Y
        _contexto.MovY = -10.0f;
    }

    public override void IniciarSubestado()
    {
        // Comprobamos si hay algun enemigo fijado
        if (_contexto.EnemigoFijado)
        { AsignarSubestado(_fabrica.EnemigoFijado()); }
        // Si no es combate libre
        else
        { AsignarSubestado(_fabrica.CombateLibre()); }
    }

    public override void SalirEstado() {}

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Raiz: En Tierra Combate");
        // Actualzamos el estado hijo
        _subestadoActual.UpdateEstado();
        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
