using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comprar : MonoBehaviour
{
    Carrito cart;
    GameObject monedas;
    MaquinaDeEstadosJugador jugador;
    // Monedas actuales
    int coins;
    InteraccionTenderoNPC interaccionTenderoNPC;
    // Start is called before the first frame update
    void Start()
    {
        // Instancia del carrito
        cart = FindObjectOfType<Carrito>();
        monedas = GameObject.Find("NumeroMonedas");
        jugador = FindObjectOfType<MaquinaDeEstadosJugador>();
        interaccionTenderoNPC = FindObjectOfType<InteraccionTenderoNPC>();
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
        // Si tengo monedas para comprar
        if (coins > 0) {
            // Obtengo el total a pagar de lo que quiero
            int totalPrice = cart.getCartPrice();
            print("PRECIO TOTAL: " + totalPrice);
            // Si tengo sufiecientes monedas para comprarlo
            if (coins >= totalPrice)
            {
                coins = coins - totalPrice;
                monedas.GetComponent<TMPro.TextMeshProUGUI>().SetText(coins.ToString());
                int[] items = cart.getCart();
                addItemsToInventory(items);
                resetQuantity();
            }
            // No tengo sufiecientes monedas
            else
            {
                // Dialogo del NPC indicandote que no tienes monedas suficientes
                interaccionTenderoNPC.SetNumSentence(2);
            }
        }
    }

    void addItemsToInventory(int[] items) {
        jugador.anyadirItemVida(items[0]);
        jugador.anyadirItemVidaPlus(items[1]);
        jugador.anyadirItemEnergia(items[2]);
        jugador.anyadirItemEnergiaPlus(items[3]);
    }

    void resetQuantity() {
        GameObject[] quantityLabels = GameObject.FindGameObjectsWithTag("LabelCantidad");
        foreach (GameObject qunatityLabel in quantityLabels) {
            qunatityLabel.GetComponent<TMPro.TextMeshProUGUI>().SetText("0");
        }
    }
}
