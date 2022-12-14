using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasosFauno : MonoBehaviour
{
    private FaunoSoundManager faunoSound;

    private void Awake()
    {
        faunoSound = GetComponent<FaunoSoundManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Paso()
    {
        faunoSound.paso();
    }
}
