using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerVida : MonoBehaviour
{
    MaquinaDeEstadosJugador maquinaDeEstadosJugador;
    // Start is called before the first frame update
    private void Awake()
    {
        maquinaDeEstadosJugador = FindObjectOfType<MaquinaDeEstadosJugador>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void vidaRecogida()
    {
        maquinaDeEstadosJugador.anyadirItemVida(1);
    }

    public void vidaPlusRecogida()
    {
        maquinaDeEstadosJugador.anyadirItemVidaPlus(1);
    }
}
