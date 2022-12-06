using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecioItems : MonoBehaviour
{
    int priceVida = 5;
    int priceVidaPlus = 30;
    int priceEnergia = 10;
    int priceEnergiaPlus = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getPriceVida() {
        return priceVida;
    }
    public int getPriceVidaPlus() {
        return priceVidaPlus;
    }
    public int getPriceEnergia() {
        return priceEnergia;
    }
    public int getPriceEnergiaPlus() {
        return priceEnergiaPlus;
    }
}
