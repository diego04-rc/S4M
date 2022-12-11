using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionTenderoNPC : MonoBehaviour
{
    GameObject menuTienda;
    GameObject textHelp;
    GameObject dialogNPC;
    GameObject VcamTienda;
    GameObject hud;
    ControladorDialogo controladorDialogo;
    MaquinaDeEstadosJugador maquinaDeEstadosJugador;
    public int numSentence = 0;
    bool enRango = false;

    private void Awake()
    {
        //Busca la VCamara de la tienda
        VcamTienda = GameObject.Find("VCamTienda");
        VcamTienda.SetActive(false);

        menuTienda = GameObject.Find("MenuTienda");
        textHelp = GameObject.FindGameObjectWithTag("TextoDeAyuda");
        dialogNPC = GameObject.Find("DialogoNPC");
        controladorDialogo = FindObjectOfType<ControladorDialogo>();
        hud = GameObject.Find("HUD");
        maquinaDeEstadosJugador = FindObjectOfType<MaquinaDeEstadosJugador>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Busca la VCamara de la tienda
        //VcamTienda = GameObject.Find("VCamTienda");
        //VcamTienda.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enRango) {
            if (Input.GetKeyDown(KeyCode.E) && numSentence == 0)
            {
                DesactiveTextHelp();
                ActiveDialogoNPC();
                ActiveCameraStore();
                controladorDialogo.Sentence(numSentence);
                numSentence++;
                maquinaDeEstadosJugador.ControladorJugador.enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.R) && numSentence == 1)
            {
                DesactiveVisibilityMenuTienda();
                ActiveDialogoNPC();
                DesactiveCameraStore();
                controladorDialogo.Sentence(numSentence);
                maquinaDeEstadosJugador.ControladorJugador.enabled = true;
            }
            else if (numSentence == 2)
            {
                ActiveDialogoNPC();
                controladorDialogo.Sentence(numSentence);
                numSentence--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            ActiveTextHelp();
            enRango = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Jugador")
        {
            DesactiveTextHelp();
            DesactiveDialogoNPC();
            enRango = false;
        }
    }

    public void ActiveVisibilityMenuTienda() {
        menuTienda.SetActive(true);
    }

    private void DesactiveVisibilityMenuTienda() {
        menuTienda.SetActive(false);
    }

    public void ActiveDialogoNPC()
    {
        dialogNPC.SetActive(true);
    }

    public void DesactiveDialogoNPC()
    {
        dialogNPC.SetActive(false);
    }
    public void ActiveTextHelp()
    {
        textHelp.SetActive(true);
    }

    public void DesactiveTextHelp()
    {
        textHelp.SetActive(false);
    }


    public void SetNumSentence(int n) {
        numSentence = n;
    }
    //Activar VCam
    public void ActiveCameraStore()
    {
        hud.SetActive(false);
        VcamTienda.SetActive(true);
    }
    //Desactivar VCam
    public void DesactiveCameraStore()
    {
        hud.SetActive(true);
        VcamTienda.SetActive(false);
    }
}
