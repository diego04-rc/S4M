using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comprar : MonoBehaviour
{
    Carrito cart;
    GameObject monedas;
    // Monedas actuales
    int coins;
    // Start is called before the first frame update
    void Start()
    {
        // Instancia del carrito
        cart = FindObjectOfType<Carrito>();
        monedas = GameObject.Find("NumeroMonedas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pulsar en el boton comprar
    void buy() {
        // Obtengo el numero de monedas del campo de texto de la interfaz
        string coinsText = monedas.GetComponent<TMPro.TextMeshProUGUI>().text;
        // Lo convierto en un entero
        int.TryParse(coinsText, out coins);
        // Si tnego monedas para comprar
        if (coins > 0) {
            // Obtengo el total a pagar de lo que quiero
            int totalPrice = cart.getCartPrice();
            print("PRECIO TOTAL: " + totalPrice);
            // Si tengo sufiecientes monedas para comprarlo
            if (coins >= totalPrice)
            {
                coins = coins - totalPrice;
                monedas.GetComponent<TMPro.TextMeshProUGUI>().SetText(coins.ToString());
                cart.getCart();
            }
            // No tengo sufiecientes monedas
            else
            {
                print("DINERO INSUFICIENTE");
            }
        }
    }
}
