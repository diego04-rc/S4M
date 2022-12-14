using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerEnergia : MonoBehaviour
{

    MaquinaDeEstadosJugador maquinaDeEstadosJugador;
    private void Awake()
    {
        maquinaDeEstadosJugador = FindObjectOfType<MaquinaDeEstadosJugador>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void energiaRecogida()
    {
        maquinaDeEstadosJugador.anyadirItemEnergia(1);
    }

    public void energiaPlusRecogida()
    {
        maquinaDeEstadosJugador.anyadirItemEnergiaPlus(1);
    }
}
