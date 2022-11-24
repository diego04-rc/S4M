using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEsquivarCombateJugador : EstadoJugador
{
    // Guardamos el contexto actual y la fabrica a traves del constructor de
    // estado jugador
    public EEsquivarCombateJugador(MaquinaDeEstadosJugador contextoActual,
        FabricaDeEstadosJugador fabrica) : base(contextoActual, fabrica) { }

    public override void ComprobarCambioEstado()
    {
        // Si ha acabado el esquive volvemos al combate
        if (_contexto.VelActEsquive < 0.0f)
        {
            // Comprobamos si esta andando y corriendo
            if (_contexto.Andando && _contexto.Corriendo)
            {
                if (_contexto.ReducirEstamina(_contexto.CosteEstaminaCorrerPorSegundo * Time.deltaTime))
                { CambiarEstado(_fabrica.CorriendoCombate()); }
            }
            // Si no, si esta andando
            else if (_contexto.Andando)
            { CambiarEstado(_fabrica.AndandoCombate()); }
            // Si no, esta quieto
            else
            { CambiarEstado(_fabrica.QuietoCombate()); }
        }

    }

    public override void EntrarEstado()
    {
        // Establecemos el estado hoja actual
        _contexto.EstadoHojaActual = MaquinaDeEstadosJugador.EstadoHoja.EsquivarCombate;

        // Si el vector input es cero, entonces hacia atras
        if (_contexto.VectorInput == Vector3.zero)
        { _contexto.DirEsquive = - Camera.main.transform.forward; }
        // Si no es el vector input
        else
        { 
            _contexto.DirEsquive = _contexto.VectorInput.normalized;
            _contexto.ModeloPersonaje.forward = new Vector3(_contexto.DirEsquive.x, 0.0f, _contexto.DirEsquive.z);
        }        

        // Iniciamos la velocidad de esquive a la maxima
        _contexto.VelActEsquive = _contexto.VelMaxEsquive;

        // Aplicamos el vector movimiento
        _contexto.MovFinal = _contexto.DirEsquive * _contexto.VelActEsquive;
    }

    public override void IniciarSubestado() {}

    public override void SalirEstado()
    {
        // Timer antes de poder realizar un esquive
        _contexto.IniciarCoolDown();
    }

    public override void UpdateEstado()
    {
        // DEBUG //
        Debug.Log("Estado hoja: Esquivar Combate");

        // Actualizamos la velocidad
        _contexto.VelActEsquive -= _contexto.DecVelEsquive * Time.deltaTime;

        Debug.Log(_contexto.VelActEsquive);

        // Aplicamos el movimiento si queda velocidad
        if (_contexto.VelActEsquive > 0.0f)
            _contexto.MovFinal = _contexto.DirEsquive * _contexto.VelActEsquive;

        // Comprobamos posibles cambios de estado
        ComprobarCambioEstado();
    }

    // Metodo para llevar a cabo el cooldown el esquive
    
}
