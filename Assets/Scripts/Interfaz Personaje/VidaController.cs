using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaController : MonoBehaviour
{
    public GameObject[] trozosDeVida;
    private int vida;
    // Posicion del trozo de vida en el array
    private int trozoDeVidaActual;
    // Start is called before the first frame update
    void Start()
    {
        vida = trozosDeVida.Length;
        trozoDeVidaActual = trozosDeVida.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Recibo daño
        {
            print(trozoDeVidaActual);
            if (trozoDeVidaActual == 0) // Muerte
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                trozosDeVida[trozoDeVidaActual].SetActive(false);
                trozoDeVidaActual--;
                vida--;
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) // Recupera vida
        {
            if (vida < trozosDeVida.Length) // No tenemos la vida completa
            {
                vida++;
                trozoDeVidaActual++;
                trozosDeVida[trozoDeVidaActual].SetActive(true);
            }
        }
    }
}
