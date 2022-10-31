using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EInteractuandoConEntornoJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador, establecemos como nodo raiz y iniciamos el estado hijo
    public EInteractuandoConEntornoJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) 
    { _esEstadoRaiz = true; IniciarSubestado(); }

    public override void ComprobarCambioEstado()
    {
        if (_contexto.InventarioAbierto && !_contexto.EnCoolDownInventario)
        { CambiarEstado(_fabrica.EnTierraExploracion()); }
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado padre
        _contexto.EstadoPadreActual = MaquinaDeEstadosJugador.EstadoPadre.InteractuandoConEntorno;

        // Nos aseguramos de que no hay movimiento
        _contexto.MovFinal = Vector3.zero;

        // Iniciamos el cooldown del inventario
        _contexto.IniciarCoolDownInventario();

        // Cambiamos la camara
        _contexto.CamaraInventario.SetActive(true);
    }

    public override void IniciarSubestado() {}

    public override void SalirEstado()
    {
        // Volvemos a la camara principal
        _contexto.CamaraInventario.SetActive(false);

        // Iniciamos el cooldown del inventario
        _contexto.IniciarCoolDownInventario();
    }

    public override void UpdateEstado()
    {
        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
