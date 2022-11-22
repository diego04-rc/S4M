using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jugar() {
        SceneManager.LoadScene("Prologo");
    }

    public void Creditos() {
        SceneManager.LoadScene("Creditos");
    }
}
