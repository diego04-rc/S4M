using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    GameObject textHelp;
    GameObject menuTienda;
    GameObject dialogNPC;
    GameObject selectLevel;
    GameObject panelFragmentos;
    private void Awake()
    {
        textHelp = GameObject.FindGameObjectWithTag("TextoDeAyuda");
        selectLevel = GameObject.Find("ElegirNivel");
        menuTienda = GameObject.Find("MenuTienda");
        dialogNPC = GameObject.Find("DialogoNPC");
        panelFragmentos = GameObject.Find("PanelFragmentos");
    }
    private void Start()
    {
        textHelp.SetActive(false);
        selectLevel.SetActive(false);
        menuTienda.SetActive(false);
        dialogNPC.SetActive(false);
        panelFragmentos.SetActive(false);
    }
}
