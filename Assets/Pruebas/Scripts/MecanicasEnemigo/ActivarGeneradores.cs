using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarGeneradores : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> generadores;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject g in generadores)
            {
                g.GetComponent<EnemyGenerator>().setActivo(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject g in generadores)
            {
                g.GetComponent<EnemyGenerator>().setActivo(false);
            }
        }
    }
}
