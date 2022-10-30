using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class EstadoJugador
{
    // Variable para indicar que es nodo raiz
    protected bool _esEstadoRaiz = false;

    // Maquina de estados actual del jugador
    protected MaquinaDeEstadosJugador _contexto;

    // Fabrica para generar todos los posibles estados
    protected FabricaDeEstadosJugador _fabrica;

    // Posible estado hijo del estado actual
    protected EstadoJugador _subestadoActual;

    // Posible estado padre del estado actual
    protected EstadoJugador _superestadoActual;

    // Creamos un estado a partir de la maquina de estados actual y
    // una fabrica de estados
    public EstadoJugador(MaquinaDeEstadosJugador contextoActual, 
        FabricaDeEstadosJugador fabricaDeEstados)
    {   _contexto = contextoActual; _fabrica = fabricaDeEstados; }

    // Metodo que se ejecuta cuando se entra a un estado
    public abstract void EntrarEstado();
    
    // Metodo que se ejecuta cada frame y updatea el estado
    public abstract void UpdateEstado();
    
    // Metodo que se ejecuta al salir de un estado
    public abstract void SalirEstado();
    
    // Metodo para comprobar un posible cambio de estado
    public abstract void ComprobarCambioEstado();
    
    // Metodo para comprobar posibles subestados
    public abstract void IniciarSubestado();

    // Metodo para cambiar de un estado actual a un nuevo estado
    protected void CambiarEstado(EstadoJugador nuevoEstado) 
    {
        // Primero abandonamos el estado actual
        SalirEstado();

        // Luego entramos en el nuevo estado
        nuevoEstado.EntrarEstado();

        // Cambiamos el estado en el contexto si somos raiz
        if (_esEstadoRaiz)
        { _contexto.EstadoActual = nuevoEstado; }
        // Si somos subestado, le decimos al padre que cambie de subestado
        else if (_superestadoActual != null)
        { _superestadoActual.AsignarSubestado(nuevoEstado); }
    }

    // Metodo para asignar un estado padre
    protected void AsignarSuperestado(EstadoJugador nuevoSuperestado) 
    { _superestadoActual = nuevoSuperestado; }

    // Metodo para asignar un estado hijo
    protected void AsignarSubestado(EstadoJugador nuevoSubestado) 
    {
        _subestadoActual = nuevoSubestado;
        nuevoSubestado.AsignarSuperestado(this);
    }
}