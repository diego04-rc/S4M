using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteraccionNave : MonoBehaviour
{
    GameObject textHelp;
    bool enRango = false;
    public bool muestroMapa = false;
    GameObject selectLevel;
    GameObject hud;
    MaquinaDeEstadosJugador maquinaDeEstadosJugador;

    private void Awake()
    {
        maquinaDeEstadosJugador = FindObjectOfType<MaquinaDeEstadosJugador>();
        textHelp = GameObject.FindGameObjectWithTag("TextoDeAyuda");
        selectLevel = GameObject.Find("ElegirNivel");
        hud = GameObject.Find("HUD");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enRango)
        {
            if (Input.GetKeyDown(KeyCode.E) && muestroMapa == false)
            {
                DesactiveTextHelp();
                muestroMapa = true;
                selectLevel.SetActive(true);
                hud.SetActive(false);
                maquinaDeEstadosJugador.ControladorJugador.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            enRango = true;
            ActiveTextHelp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Jugador")
        {
            enRango = false;
            DesactiveTextHelp();
            hud.SetActive(true);
            muestroMapa = false;
        }
    }

    public void ActiveTextHelp()
    {
        textHelp.SetActive(true);
    }

    public void DesactiveTextHelp()
    {
        textHelp.SetActive(false);
    }
}
