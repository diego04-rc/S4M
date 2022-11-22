using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscenas : MonoBehaviour
{
    public Animator animator;
    private int seconds = 9;
    public GameObject[] imgPrologo;
    private int puntero;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        puntero = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (i == 0) {
            StartCoroutine(CambiarImagen());
            i++;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameSecene();
        }
    }

    IEnumerator CambiarImagen() {
        while (puntero < imgPrologo.Length) {
            if (puntero == 5) { seconds = 5; }
            FadeIn();
            Invoke("FadeOut", seconds - 1);
            yield return new WaitForSeconds(seconds);
            imgPrologo[puntero].SetActive(false);
            puntero++;
            if (puntero < imgPrologo.Length)
            {
                imgPrologo[puntero].SetActive(true);
            }
            else { GameSecene(); }
        }
    }
    public void FadeOut() {
        animator.Play("FadeOut");
    }

    public void FadeIn() {
        animator.Play("FadeIn");
    }

    public void GameSecene() {
        SceneManager.LoadScene("World1");
    }
}
