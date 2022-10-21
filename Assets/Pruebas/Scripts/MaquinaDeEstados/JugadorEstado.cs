using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class JugadorEstado
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void EntrarEstado();
    
    public abstract void UpdateEstado();
    
    public abstract void SalirEstado();
    
    public abstract void ComprobarCambioEstado();
    
    public abstract void IniciarSubestado();

    void UpdateEstados() { }

    void CambiarEstado() { }

    void AsignarSuperestado() { }

    void AsignarSubestado() { }
}
