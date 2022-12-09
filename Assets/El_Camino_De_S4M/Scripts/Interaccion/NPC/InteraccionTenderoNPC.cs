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
    ControladorDialogo controladorDialogo;
    public int numSentence = 0;
    public bool moveCameraToMenuTienda = false;
    bool enRango = true;

    private void Awake()
    {
        //Busca la VCamara de la tienda
        VcamTienda = GameObject.Find("VCamTienda");
        VcamTienda.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        menuTienda = GameObject.Find("MenuTienda");
        textHelp = GameObject.Find("InteraccionNPC");
        dialogNPC = GameObject.Find("DialogoNPC");
        controladorDialogo = FindObjectOfType<ControladorDialogo>();
        //Busca la VCamara de la tienda
        //VcamTienda = GameObject.Find("VCamTienda");
        //VcamTienda.SetActive(false);
        DesactiveVisibilityMenuTienda();
        DesactiveTextHelp();
        DesactiveDialogoNPC();
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
                moveCameraToMenuTienda = true;
            }
            else if (Input.GetKeyDown(KeyCode.R) && numSentence == 1)
            {
                DesactiveVisibilityMenuTienda();
                ActiveDialogoNPC();
                DesactiveCameraStore();
                controladorDialogo.Sentence(numSentence);
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
        VcamTienda.SetActive(true);
    }
    //Desactivar VCam
    public void DesactiveCameraStore()
    {
        VcamTienda.SetActive(false);
    }
}
