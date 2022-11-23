using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorSplash : MonoBehaviour
{
    // Escena del menu principal
    [Tooltip("Escena del menu principal")]
    [SerializeField]
    private string _menuPrincipal;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(_menuPrincipal);
        }
    }
}
