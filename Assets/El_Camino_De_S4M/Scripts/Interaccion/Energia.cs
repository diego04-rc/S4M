using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energia : MonoBehaviour
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
            //A�adir al inventario
            //other.GetComponent<RecogerEnergia>().recogerEnergia();
            Destroy(gameObject);
        }
    }
}
