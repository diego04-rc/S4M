using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEjemplo : Ataque
{
    private CharacterSoundManager characterSoundManager;

    private void Awake()
    {
        characterSoundManager = GetComponentInChildren<CharacterSoundManager>();
    }
    protected override void EfectoAtaque(List<GameObject> objetosEnTrigger)
    {
        Debug.Log("Efectos aplicados a: ");
        foreach(GameObject objeto in objetosEnTrigger)
        {
            Destroy(objeto);
        }
        characterSoundManager.atacar();
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
