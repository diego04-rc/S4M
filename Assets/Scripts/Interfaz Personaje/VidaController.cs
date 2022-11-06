using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaController : MonoBehaviour
{
    // Contiene los trozos de la vida del personaje
    public GameObject[] imgVida;
    // Contiene las caras del HUD del personaje
    public GameObject[] imgS4M;

    public int tiempoCambio = 1; // Controla el timepo que se muestra la cara de daño
    private int vida; // Variable para controlar la vida
    private readonly int vidaMax = 4;
    // Posicion del trozo de vida en el array
    private int punteroVida;

    void Start()
    {
        vida = vidaMax;
        punteroVida = imgVida.Length - 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Recibo daño
        {
            if (vida == 1) // Muerte
            {
                imgVida[punteroVida].SetActive(false);
                StartCoroutine(Reiniciar());
            }
            else
            {
                imgVida[punteroVida].SetActive(false);
                punteroVida--;
                imgVida[punteroVida].SetActive(true);
                vida--;
                StartCoroutine(CambiarCara());
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) // Recupera vida
        {
            if (vida < vidaMax) // No tenemos la vida completa
            {
                vida++;
                imgVida[punteroVida].SetActive(false);
                punteroVida++;
                imgVida[punteroVida].SetActive(true);
            }
        }
    }

    IEnumerator CambiarCara() {
        imgS4M[0].SetActive(false);
        imgS4M[1].SetActive(true);
        yield return new WaitForSeconds(tiempoCambio);
        imgS4M[1].SetActive(false);
        imgS4M[0].SetActive(true);
    }

    IEnumerator Reiniciar() {
        imgS4M[0].SetActive(false);
        imgS4M[2].SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
