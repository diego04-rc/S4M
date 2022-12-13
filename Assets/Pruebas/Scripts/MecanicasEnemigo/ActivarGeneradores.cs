using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarGeneradores : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> generadores;
    [SerializeField]
    private int maxEnemigos;
    private List<GameObject> enemigosEnZona;
    void Start()
    {
        enemigosEnZona = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject g in enemigosEnZona)
        {
            if (g == null) enemigosEnZona.Remove(g);
        }
        if (enemigosEnZona.Count >= maxEnemigos)
        {
            desactivarGeneradores();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(enemigosEnZona.Count < maxEnemigos)
            activarGeneradores();
        }
        if (other.CompareTag("Enemigo"))
        {
            enemigosEnZona.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            desactivarGeneradores();
        }
        if (other.CompareTag("Enemigo"))
        {
            enemigosEnZona.Remove(other.gameObject);
        }
    }

    private void activarGeneradores()
    {
        foreach (GameObject g in generadores)
        {
            g.GetComponent<EnemyGenerator>().setActivo(true);
        }
    }

    private void desactivarGeneradores()
    {
        foreach (GameObject g in generadores)
        {
            g.GetComponent<EnemyGenerator>().setActivo(false);
        }
    }
}
