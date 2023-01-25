using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecogerMonedas : MonoBehaviour
{
    private int monedas = 100;
    public TextMeshProUGUI nMonedas;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        nMonedas.SetText(monedas.ToString());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void monedaRecogida() {
        monedas++;
        nMonedas.SetText(monedas.ToString());
        audioSource.Play();
    }
}
