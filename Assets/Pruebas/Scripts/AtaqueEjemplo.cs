using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEjemplo : Ataque
{
    protected override void EfectoAtaque(List<GameObject> objetosEnTrigger)
    {
        Debug.Log("Efectos aplicados a: ");
        foreach(GameObject objeto in objetosEnTrigger)
        {
            Debug.Log(objeto.name);
        }
    }

    protected override void PostAtaque()
    {
        Debug.Log("El ataque ha acabado");
    }

    protected override void PrevioAtaque()
    {
        Debug.Log("El ataque ha comenzado.");
    }
}
