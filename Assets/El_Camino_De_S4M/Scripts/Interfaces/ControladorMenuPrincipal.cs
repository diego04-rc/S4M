using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
{
    // Menu principal
    [Tooltip("Gameobject del menu principal")]
    [SerializeField]
    private GameObject _menuPrincipal;

    // Menu opciones
    [Tooltip("Gameobject del menu de opciones")]
    [SerializeField]
    private GameObject _menuOpciones;


    // Callback del boton jugar
    public void botonJugarPulsado()
    {
        // Abrimos la escena del prologo
        SceneManager.LoadScene("Prologo");
    }

    // Callback del boton de opciones
    public void botonOpcionesPulsado()
    {
        // Desactivamos el menu principal
        _menuPrincipal.SetActive(false);

        // Activamos el menu de opciones
        _menuOpciones.SetActive(true);
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

    // Callback del boton de volver de opciones
    public void botonVolverPulsado()
    {
        // Desactivamos las opciones
        _menuOpciones.SetActive(false);

        // Activamos el menu principal
        _menuPrincipal.SetActive(true);
    }
}
