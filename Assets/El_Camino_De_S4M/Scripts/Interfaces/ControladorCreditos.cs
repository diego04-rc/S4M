using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCreditos : MonoBehaviour
{
    // Duracion de la pantalla de creditos
    [Tooltip("Tiempo que dura la pantalla de creditos")]
    [SerializeField]
    private float _duracionCreditos;

    // Nombre de la escena de salida
    [Tooltip("Nombre de la escena a la que dirigen los creditos")]
    [SerializeField]
    private string _nombreEscenaSalida;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CronometroCreditos());
    }

    // Update is called once per frame
    void Update()
    {
        // Si se pulsa cualquier tecla se finalizan los creditos
        if (Input.anyKeyDown)
        {
            FinalizarCreditos();
        }
    }

    // Cuenta atras para finalizar los creditos
    IEnumerator CronometroCreditos()
    {
        yield return new WaitForSeconds(_duracionCreditos);
        FinalizarCreditos();
    }
    
    // Metodo que finaliza los creditos y cambia de escena
    void FinalizarCreditos()
    {
        SceneManager.LoadScene(_nombreEscenaSalida);
    }
}
