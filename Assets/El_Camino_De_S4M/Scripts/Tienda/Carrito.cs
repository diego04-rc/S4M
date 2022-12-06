using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrito : MonoBehaviour
{
    PrecioItems precioItems;
    // Cesta de la "compra"
    int[] basket = new int[4];
    // Cantidad de cada item que puedes comprar
    public int quantityVida = 0;
    public int quantityVidaPlus = 0;
    public int quantityEnergia = 0;
    public int quantityEnergiaPlus = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Instancia de la clase PrecioItems (solamente indica el precio de cada item)
        precioItems = FindObjectOfType<PrecioItems>();
        // Asigno una posicion en la cesta para cada tipo de item (añado cada tipo de item a la cesta)
        basket.SetValue(quantityVida, 0);
        basket.SetValue(quantityVidaPlus, 1);
        basket.SetValue(quantityEnergia, 2);
        basket.SetValue(quantityEnergiaPlus, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] getCart() {
        // Añado a la cesta la cantidad elegida de cada item
        basket.SetValue(quantityVida, 0);
        basket.SetValue(quantityVidaPlus, 1);
        basket.SetValue(quantityEnergia, 2);
        basket.SetValue(quantityEnergiaPlus, 3);
        return basket;
    }

    public int getCartPrice() {
        // Calculo el precio total de los items en la cesta
        int totalPrice = quantityVida * precioItems.getPriceVida() +
                         quantityVidaPlus * precioItems.getPriceVidaPlus() +
                         quantityEnergia * precioItems.getPriceEnergia() +
                         quantityEnergiaPlus * precioItems.getPriceEnergiaPlus();
        return totalPrice;
    }
}
