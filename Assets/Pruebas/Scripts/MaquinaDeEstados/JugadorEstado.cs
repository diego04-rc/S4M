using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class JugadorEstado
{
    protected bool _esEstadoRaiz = false;
    protected MaquinaEstadosJugador _contexto;
    protected FabricaDeEstados _fabrica;
    protected JugadorEstado _subestadoActual;
    protected JugadorEstado _superestadoActual;

    public JugadorEstado(MaquinaEstadosJugador contextoActual, 
        FabricaDeEstados fabricaDeEstados)
    {
        _contexto = contextoActual;
        _fabrica = fabricaDeEstados;
    }

    public abstract void EntrarEstado();
    
    public abstract void UpdateEstado();
    
    public abstract void SalirEstado();
    
    public abstract void ComprobarCambioEstado();
    
    public abstract void IniciarSubestado();

    void UpdateEstados() { }

    protected void CambiarEstado(JugadorEstado nuevoEstado) 
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

    protected void AsignarSuperestado(JugadorEstado nuevoSuperestado) 
    {
        _superestadoActual = nuevoSuperestado;
    }

    protected void AsignarSubestado(JugadorEstado nuevoSubestado) 
    {
        _subestadoActual = nuevoSubestado;
        nuevoSubestado.AsignarSuperestado(this);
    }
}
