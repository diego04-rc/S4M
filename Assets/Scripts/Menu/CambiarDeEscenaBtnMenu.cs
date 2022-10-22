using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarDeEscenaBtnMenu : MonoBehaviour
{
    public int escena;
    // Start is called before the first frame update
    void Start(){}
    // Update is called once per frame
    void Update(){}

    public void CambiarDeEscena() {
        SceneManager.LoadScene(escena);
    }
}
