using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    GameObject hud;
    InteraccionNave interaccionNave;
    MaquinaDeEstadosJugador maquinaDeEstadosJugador;

    private void Start()
    {
        maquinaDeEstadosJugador = FindObjectOfType<MaquinaDeEstadosJugador>();
        hud = GameObject.Find("HUD");
        interaccionNave = FindObjectOfType<InteraccionNave>();
    }
    public void CloseCanvas() {
        GameObject.Find("ElegirNivel").SetActive(false);
        hud.SetActive(true);
        interaccionNave.muestroMapa = false;
        maquinaDeEstadosJugador.ControladorJugador.enabled = true;
    }
}
