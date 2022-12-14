using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueFauno : Ataque
{
    protected override void EfectoAtaque(List<GameObject> objetosEnTrigger)
    {
        foreach(GameObject objeto in objetosEnTrigger)
        {
            if (objeto.tag == "Player")
            {
                objeto.GetComponent<MaquinaDeEstadosJugador>().RecibirDanyo();
            }
        }
    }

    protected override void PostAtaque()
    {
        
    }

    protected override void PrevioAtaque()
    {
        
    }
}
