using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerVida : MonoBehaviour
{
    MaquinaDeEstadosJugador maquinaDeEstadosJugador;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
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
        audioSource.Play();
    }

    public void vidaPlusRecogida()
    {
        maquinaDeEstadosJugador.anyadirItemVidaPlus(1);
        audioSource.Play();
    }
}
