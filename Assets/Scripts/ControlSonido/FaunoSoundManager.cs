using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaunoSoundManager : MonoBehaviour
{
    private AudioSource[] faunoSounds;

    private void Awake()
    {
        faunoSounds = GetComponents<AudioSource>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void rugido()
    {
        faunoSounds[0].Play();
    }

    public void atacar()
    {
        faunoSounds[1].Play();
    }

    public void paso()
    {
        faunoSounds[2].Play();
    }
}
