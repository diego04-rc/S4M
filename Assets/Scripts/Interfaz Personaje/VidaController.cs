using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaController : MonoBehaviour
{
    public GameObject[] imgVida;
    private int vida;
    private readonly int vidaMax = 4;
    // Posicion del trozo de vida en el array
    private int punteroVida;
    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMax;
        punteroVida = imgVida.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Recibo daño
        {
            if (punteroVida == 0) // Muerte
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                imgVida[punteroVida].SetActive(false);
                punteroVida--;
                imgVida[punteroVida].SetActive(true);
                vida--;
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
}
