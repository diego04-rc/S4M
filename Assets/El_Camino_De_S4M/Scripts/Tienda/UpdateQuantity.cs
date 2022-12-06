using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateQuantity : MonoBehaviour
{
    GameObject quantity;
    GameObject item;
    int value = 0;
    Carrito cart;
    // Start is called before the first frame update
    void Start()
    {
        cart = FindObjectOfType<Carrito>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    int getQuantity() {
        // Me guardo el GameObject padre (Item1, 2, 3, etc.)
        item = this.gameObject.transform.parent.gameObject;
        // Me guardo el GameObject que indica la cantidad: quantity
        quantity = this.gameObject.transform.GetChild(2).gameObject;
        // Obtengo la cantidad
        string valueText = quantity.GetComponent<TMPro.TextMeshProUGUI>().text;
        // La convierto en int
        int.TryParse(valueText, out value);
        return value;
    }

    public void increment() {
        // Recupero la cantidad actual de un item
        int q = getQuantity(); 
        if (q < 99)  // Como maximo puedo comprar 99 items de cada tipo 
        {
            // Aumento la cantidad
            value++;
            // Muestro la nueva contidad por pantalla
            quantity.GetComponent<TMPro.TextMeshProUGUI>().SetText(value.ToString());
            // Actualizo la cesta
            updateBasket();
        }
        //print(quantity.GetComponent<TMPro.TextMeshProUGUI>().text);
    }

    public void decrement()
    {
        // Recupero la cantidad actual de un item
        int q = getQuantity();
        if (q > 0) // Nunca puedo comprar menos de 0 items 
        {
            // Disminuyo la cantidad
            value--;
            // Muestro la nueva contidad por pantalla
            quantity.GetComponent<TMPro.TextMeshProUGUI>().SetText(value.ToString());
            // Actualizo la cesta
            updateBasket();
        }
        //print(quantity.GetComponent<TMPro.TextMeshProUGUI>().text);
    }

    void updateBasket() {
        // Añado un item a la cantidad deseada
        if (value != 0)
        {
            // Dependiendo del item, asigo el valor en la cesta donde le corresponde
            if (item.name == "Item1") {
                cart.quantityVida = value;
            }
            if (item.name == "Item2") {
                cart.quantityVidaPlus = value;
            }
            if (item.name == "Item3") {
                cart.quantityEnergia = value;
            }
            if (item.name == "Item4") {
                cart.quantityEnergiaPlus = value;
            }           
        }
    }
}
