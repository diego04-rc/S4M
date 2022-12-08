using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorDialogo : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public string[] frases;
    public float velocidadDialogo;
    float speed = 1f;
    InteraccionTenderoNPC interaccionTenderoNPC;
    // Start is called before the first frame update
    void Start()
    {
        interaccionTenderoNPC = FindObjectOfType<InteraccionTenderoNPC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sentence(int numSentence) {
        if (numSentence <= frases.Length - 1) {
            dialogText.text = "";
            StartCoroutine(WriteSentence(numSentence));
        }
    }

    IEnumerator WriteSentence(int numSentence) {
        foreach (char Character in frases[numSentence].ToCharArray()) {
            dialogText.text += Character;
            yield return new WaitForSeconds(velocidadDialogo);
        }
        // Inicio animacion hacia la tienda
        if (numSentence == 0)
        {
            interaccionTenderoNPC.ActiveVisibilityMenuTienda();
            yield return new WaitForSeconds(speed);
            interaccionTenderoNPC.DesactiveDialogoNPC();
        }
        else if (numSentence != 0) {
            yield return new WaitForSeconds(speed);
            interaccionTenderoNPC.DesactiveDialogoNPC();
            interaccionTenderoNPC.SetNumSentence(0);
        }
    }
}
