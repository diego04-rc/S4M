using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECorriendoAireCombateJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador
    public ECorriendoAireCombateJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        // Si dejamos de andar pasamos a quieto
        if (!_contexto.Andando)
        { CambiarEstado(_fabrica.QuietoAireCombate()); }
        // Si dejamos de correr pasamos a andar
        else if (!_contexto.Corriendo)
        { CambiarEstado(_fabrica.AndandoAireCombate()); }
    }

    public override void EntrarEstado()
    {
        // Asignamos la velocidad a la minima corriendo
        _contexto.VelActual = _contexto.VelMinCorriendo;
        // Iniciamos el vector movimiento final
        _contexto.MovFinal = _contexto.VectorInput * _contexto.VelActual;
    }

    public override void IniciarSubestado() { }

    public override void SalirEstado() { }

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Hoja: Corriendo Aire Fijado");

        // Si la velocidad no es la maxima, aumentamos con el incremento
        if (_contexto.VelActual < _contexto.VelMaxCorriendo)
        { _contexto.VelActual += _contexto.IncVelCorriendo * Time.deltaTime; }
        if (_contexto.VelActual > _contexto.VelMaxCorriendo)
        { _contexto.VelActual = _contexto.VelMaxCorriendo; }

        // Iniciaos el vector de movimiento final
        _contexto.MovFinal = _contexto.VectorInput * _contexto.VelActual;

        // Corregimos la direccíon a la que mira el personaje
        Vector3 dirObjetivo = _contexto.MovFinal;
        Vector3 direccion = Vector3.RotateTowards(_contexto.ModeloPersonaje.forward,
            dirObjetivo, _contexto.VelDirCorriendo * Time.deltaTime, 0.0f);
        direccion.y = 0.0f;
        _contexto.ModeloPersonaje.rotation = Quaternion.LookRotation(direccion);

        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();
    }
}
