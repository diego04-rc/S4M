using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSimple : MonoBehaviour
{
    private Ataque _miAtaque;

    // Start is called before the first frame update
    void Start()
    {
        _miAtaque = GetComponent<Ataque>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _miAtaque.Atacar();
        }
    }
}
