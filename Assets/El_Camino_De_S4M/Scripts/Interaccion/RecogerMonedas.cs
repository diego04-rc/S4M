using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecogerMonedas : MonoBehaviour
{
    private int monedas = 0;
    TextMeshPro nMonedas;
    // Start is called before the first frame update
    void Start()
    {
        nMonedas = GameObject.Find("NumeroMonedas").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void monedaRecogida() {
        monedas++;
        nMonedas.SetText(monedas.ToString());
    }
}
