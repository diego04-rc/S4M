using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    private bool menuOn;
    // Start is called before the first frame update
    void Start(){   }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            menuOn = !menuOn;
        }
        if (menuOn)
        {
            menuPausa.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else {
            menuPausa.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Continuar() {
        menuPausa.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuOn = false;
    }

    public void Opciones() { }

    public void Salir() {
        SceneManager.LoadScene("MainMenu");
    }
}

