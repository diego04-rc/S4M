using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            //Añadir al inventario
            //other.GetComponent<RecogerVida>().recogerVida();
            Destroy(gameObject);
        }
    }
}
