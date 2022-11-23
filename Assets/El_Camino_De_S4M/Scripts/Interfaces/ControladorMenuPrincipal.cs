using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
{
    // Callback del boton jugar
    public void botonJugarPulsado()
    {
        // Abrimos la escena del prologo
        SceneManager.LoadScene("Prologo");
    }

    // Callback del boton de opciones
    public void botonOpcionesPulsado()
    {
        // Quitamos la visibilidad del menu

    }

    // Callback del boton de salir
    public void botonSalirPulsado()
    {
        // Cerramos el juego
        Application.Quit();
    }

    // Callback del boton de creditos
    public void botonCreditosPulsado()
    {
        // Abrimos la escena de los creditos
        SceneManager.LoadScene("Creditos");
    }
}
