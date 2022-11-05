using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    // Variables iniciales, costes de la stamina y regeneracion
    public float staminaActual;
    private float staminaMax = 100.0f;
    [SerializeField] private float costeSalto = 10.0f;
    [SerializeField] private float costeCorrer = 5.0f;
    [SerializeField] private float costeAtaqueLigero = 12.5f;
    [SerializeField] private float costeAtaquePesado = 22.0f;
    [SerializeField] private float costeEsquive = 10.0f;
    [SerializeField] private float regeneracionDeStamina = 0.5f;

    // Barra de stamina
    private Image staminaUI = null;

    // Variable para el estado
    private MaquinaDeEstadosJugador _estado;

    void Start()
    {
        // Para que la stamina nunca exceda la staminaMax ni sea menor de 0
        staminaActual = Mathf.Clamp(staminaActual, 0, staminaMax);
        staminaActual = staminaMax;
        staminaUI = GameObject.Find("Image_Stamina").GetComponent<Image>();
        // Recuperar stamina
        StartCoroutine(RecuperarStamina());
    }

    
    // Actualizacion de la stamina
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            ReducirStamina(costeEsquive);
        }
        /*
        // Modo Exploración
        if (estadoPersonaje.EstadoPadreActual == MaquinaDeEstadosJugador.EstadoPadre.TierraExploracion)
        {
            // Corriendo
            if (estadoPersonaje.EstadoHojaActual  == MaquinaDeEstadosJugador.EstadoHoja.Corriendo)
            {
                ReducirStamina(costeCorrer);
            }
        }
        // Modo Exploración Aire
        else if (estadoPersonaje.EstadoPadreActual == MaquinaDeEstadosJugador.EstadoPadre.AireExploracion)
        {
            // Corriendo
            if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.CorriendoAire)
            {
                ReducirStamina(costeCorrer);
            }
        }
        // Modo Combate en Tierra
        else if (estadoPersonaje.EstadoPadreActual == MaquinaDeEstadosJugador.EstadoPadre.TierraCombate)
        {
            // Corriendo
            if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.CorriendoCombate)
            {
                ReducirStamina(costeCorrer);
            }
            // Corriendo fijando enemigo
            else if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.CorriendoCombateFijando)
            {
                ReducirStamina(costeCorrer);
            }
            // Ataque
            else if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.AtacarCombate)
            {
                //Reducir stamina
                if (estadoPersonaje.AtaquePesado == true) // Ataque pesado
                { ReducirStamina(costeAtaquePesado); }
                else if (estadoPersonaje.AtaqueLigero == true) // Ataque ligero
                { ReducirStamina(costeAtaqueLigero); }
            }
            // Ataque fijando enemigo
            else if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.AtacarCombateFijando)
            {
                //Reducir stamina
                if (estadoPersonaje.AtaquePesado == true) // Ataque cargado
                { ReducirStamina(costeAtaquePesado); }
                else if(estadoPersonaje.AtaqueLigero == true)  // Ataque ligero
                { ReducirStamina(costeAtaqueLigero); }
            }
            // Esquivar
            else if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.EsquivarCombate)
            {
                ReducirStamina(costeEsquive);
            }
            // Esquivar fijando enemigo
            else if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.EsquivarCombateFijando)
            {
                ReducirStamina(costeEsquive);
            }
        }
        // Modo Combate Aire
        else if (estadoPersonaje.EstadoPadreActual == MaquinaDeEstadosJugador.EstadoPadre.AireCombate)
        {
            // Corriendo
            if (estadoPersonaje.EstadoHojaActual == MaquinaDeEstadosJugador.EstadoHoja.CorriendoAireCombate)
            {
                ReducirStamina(costeCorrer);
            }
        }*/
        
    }

    private void ReducirStamina(float reduccion) 
    {
        // Si no tienes stamina
        if (staminaActual <= 1.0f)
        {
            // Reducir velocidad de movimiento
            // Solo permitir ataques ligeros
        }
        else if(staminaActual >= reduccion){
            staminaActual -= reduccion;
        }
        ActualizarStamina();
    }

    IEnumerator RecuperarStamina() 
    {
        while (true) {
            yield return new WaitForSeconds(0.05f);
            if (staminaActual < staminaMax)
            {
                staminaActual += regeneracionDeStamina;
                ActualizarStamina();
            }
        }
    }

    private void ActualizarStamina() 
    {
        staminaUI.fillAmount = staminaActual / staminaMax;
    }
}
