using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQuietoCombateJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador
    public EQuietoCombateJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        // Si estamos andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { CambiarEstado(_fabrica.CorriendoCombate()); }
        // Si solo estamos andando
        else if (_contexto.Andando)
        { CambiarEstado(_fabrica.AndandoCombate()); }
        // Si se pusa saltar, se realiza un esquive si no esta en cooldown
        else if (_contexto.Saltado && !_contexto.EnCoolDownEsquive)
        { CambiarEstado(_fabrica.EsquivarCombate()); }
        // Si ha atacado
        else if (_contexto.EjecutadoAtaqueLigero || _contexto.EjecutadoAtaquePesado)
        { CambiarEstado(_fabrica.AtacarCombate()); }
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado hoja actual
        _contexto.EstadoHojaActual = MaquinaDeEstadosJugador.EstadoHoja.QuietoCombate;

        // Nos aseguramos de que el movimiento sea cero
        _contexto.MovFinal = Vector3.zero;
    }

    public override void IniciarSubestado() { }

    public override void SalirEstado() { }

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Hoja: Quieto Combate");

        Vector3 dirObjetivo = Camera.main.transform.forward;
        Vector3 direccion = Vector3.RotateTowards(_contexto.ModeloPersonaje.forward,
            dirObjetivo, _contexto.VelDirAndando * Time.deltaTime, 0.0f);
        direccion.y = 0.0f;
        _contexto.ModeloPersonaje.rotation = Quaternion.LookRotation(direccion);

        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
