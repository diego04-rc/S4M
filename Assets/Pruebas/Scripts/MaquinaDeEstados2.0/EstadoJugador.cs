using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class EstadoJugador
{
    protected bool _esEstadoRaiz = false;
    protected MaquinaDeEstadosJugador _contexto;
    protected FabricaDeEstadosJugador _fabrica;
    protected EstadoJugador _subestadoActual;
    protected EstadoJugador _superestadoActual;

    public EstadoJugador(MaquinaDeEstadosJugador contextoActual, FabricaDeEstadosJugador fabricaDeEstados)
    {
        _contexto = contextoActual;
        _fabrica = fabricaDeEstados;
    }

    public abstract void EntrarEstado();
    
    public abstract void UpdateEstado();
    
    public abstract void SalirEstado();
    
    public abstract void ComprobarCambioEstado();
    
    public abstract void IniciarSubestado();

    protected void CambiarEstado(EstadoJugador nuevoEstado) 
    {
        // Primero abandonamos el estado actual
        SalirEstado();

        // Luego entramos en el nuevo estado
        nuevoEstado.EntrarEstado();

        if (_esEstadoRaiz)
        {
            // Cambiamos el estado en el contexto
            _contexto.EstadoActual = nuevoEstado;
        }
        else if (_superestadoActual != null)
        {
            // Si somos subestado, le decimos al padre que cambie de subestado
            _superestadoActual.AsignarSubestado(nuevoEstado);
        }
    }

    protected void AsignarSuperestado(EstadoJugador nuevoSuperestado) 
    {
        _superestadoActual = nuevoSuperestado;
    }

    protected void AsignarSubestado(EstadoJugador nuevoSubestado) 
    {
        _subestadoActual = nuevoSubestado;
        nuevoSubestado.AsignarSuperestado(this);
    }
}
