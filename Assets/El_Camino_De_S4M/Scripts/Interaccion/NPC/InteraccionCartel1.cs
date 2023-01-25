using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteraccionCartel1 : MonoBehaviour
{
    GameObject dialogNPC;
    GameObject textHelp;
    GameObject hud;
    MaquinaDeEstadosJugador maquinaDeEstadosJugador;
    public int numSentence = 0;
    bool enRango = false;
    //private AudioSource saludo;
    public string[] frases;
    public TextMeshProUGUI dialogText;
    public float velocidadDialogo;
    float speed = 1f;
    private bool imprimiendo;
    private void Awake()
    {
        textHelp = GameObject.FindGameObjectWithTag("TextoDeAyuda");
        dialogNPC = GameObject.Find("DialogoNPC");
        hud = GameObject.Find("HUD");
        maquinaDeEstadosJugador = FindObjectOfType<MaquinaDeEstadosJugador>();

        //saludo = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
       imprimiendo= false;  
    }

    // Update is called once per frame
    void Update()
    {
        if (enRango)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DesactiveTextHelp();
                ActiveDialogoNPC();
                Sentence();
                //ActualizarSentence();
                maquinaDeEstadosJugador.ControladorJugador.enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                DesactiveDialogoNPC();
                maquinaDeEstadosJugador.ControladorJugador.enabled = true;
            }
        }
    }

    private void ActualizarSentence()
    {
        numSentence++;
        if (numSentence >= frases.Length)
        {
            numSentence = 0;
        }
    }

    private void DesactualizarSentence()
    {
        numSentence--;
        if (numSentence < 0)
        {
            numSentence = frases.Length - 1;
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

    public void Sentence()
    {
        if (numSentence <= frases.Length - 1 && !imprimiendo)
        {
            imprimiendo = true;
            dialogText.text = "";
            StartCoroutine(WriteSentence());
            ActualizarSentence();
        }
        else if (numSentence <= frases.Length - 1 && imprimiendo)
        {
            imprimiendo = false;
            DesactualizarSentence();
            StopCoroutine("WriteSentence");
            dialogText.text = frases[numSentence];
            ActualizarSentence();
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char Character in frases[numSentence].ToCharArray())
        {
            if (imprimiendo)
            {
                dialogText.text += Character;
                yield return new WaitForSeconds(velocidadDialogo);
            }
        }
        imprimiendo = false;
    }
}
