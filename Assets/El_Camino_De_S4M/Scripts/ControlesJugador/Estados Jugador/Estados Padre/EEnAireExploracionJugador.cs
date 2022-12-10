using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEnAireExploracionJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador, establecemos como nodo raiz y iniciamos el estado hijo
    public EEnAireExploracionJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) 
    { _esEstadoRaiz = true; IniciarSubestado(); }

    bool _tiempoEspera = true;
    float _tiempoPasado = 0.0f;

    public override void ComprobarCambioEstado()
    {
        // Comprobamos si volvemos a estar en tierra
        if (_contexto.ControladorJugador.isGrounded && !_tiempoEspera)
        {
            _contexto.animator.SetBool("Saltado", false);
            _contexto.animator.SetBool("EnElAire", false);
            CambiarEstado(_fabrica.EnTierraExploracion());
        }
    }

    public override void EntrarEstado()
    {
        // Establecemos el estado padre
        _contexto.EstadoPadreActual = MaquinaDeEstadosJugador.EstadoPadre.AireExploracion;

        _contexto.animator.SetBool("Saltado", true);

        _tiempoEspera = true;
    }

    public override void IniciarSubestado()
    {
        // Comprobamos si esta andando y corriendo
        if (_contexto.Andando && _contexto.Corriendo)
        { AsignarSubestado(_fabrica.CorriendoAire()); }
        // Si no, si esta andando
        else if (_contexto.Andando)
        { AsignarSubestado(_fabrica.AndandoAire()); }
        // Si no, esta quieto
        else
        { AsignarSubestado(_fabrica.QuietoAire()); }
    }

    public override void SalirEstado() {}

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado Raiz: En Aire Exploracion");

        if (_tiempoEspera)
        {
            _tiempoPasado += Time.deltaTime;
            if (_tiempoPasado > 0.8f)
            {
                // Comprobamos si se ha saltado para aplicar velocidad
                _contexto.MovY = _contexto.VelSalto;
                _contexto.animator.SetBool("EnElAire", true);
            }
        }
        else
        {
            // Aplicamos la gravedad al jugador
            if (_contexto.MovY > 0.0f)
            { _contexto.MovY -= _contexto.Gravedad * Time.deltaTime; }
            // Si ademas estamos cayendo, aplicamos un extra de caida
            else
            { _contexto.MovY -= _contexto.Gravedad * _contexto.IncDeCaida * Time.deltaTime; }
        }
        

        // Actualizamos el estado hijo
        _subestadoActual.UpdateEstado();
        // Comprobamos un posible cambio de estado
        ComprobarCambioEstado();

        if (_tiempoPasado > 0.8f)
        {
            _tiempoEspera = false;
        }
    }
}
