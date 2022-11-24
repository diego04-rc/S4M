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

        // El acompañante pasa a estar en posicion de inventario
        _contexto.Acompanyante.cambiarEstado(EsferaAcompanyante.EstadoAcompanyante.Inventario);

        // Reactivamos el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void IniciarSubestado() {}

    public override void SalirEstado()
    {
        // Volvemos a la camara principal
        _contexto.CamaraInventario.SetActive(false);

        // Iniciamos el cooldown del inventario
        _contexto.IniciarCoolDownInventario();

        // El acompañante vuelve a seguir
        _contexto.Acompanyante.cambiarEstado(EsferaAcompanyante.EstadoAcompanyante.Siguiendo);

        // Desactivamos el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Desactivamos el inventario
        _contexto.MenuInventario.enabled = false;
    }

    public override void UpdateEstado()
    {
        // En cuanto la bola acompanyante este en posicion, mostramos el inventario
        if (_contexto.Acompanyante.EstadoActual == 
            EsferaAcompanyante.EstadoAcompanyante.MostrandoInventario)
            _contexto.MenuInventario.enabled = true;

        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
