using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource[] characterSounds;

    private void Awake()
    {
        characterSounds = GetComponents<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void desenvainarEspada()
    {
        characterSounds[0].Play();
    }

    public void envainarEspada()
    {
        characterSounds[1].Play();
    }

    public void atacar()
    {
        characterSounds[2].Play();
    }

    public void paso()
    {
        characterSounds[3].Play();
    }

    public void saltoInicio()
    {
        characterSounds[4].Play();
    }

    public void saltoCaida()
    {
        characterSounds[5].Play();
    }
}
