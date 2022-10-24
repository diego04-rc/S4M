using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlVolumen : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void ControldelVolumen(float sliderVolumen)
    {
        audioMixer.SetFloat("Volumen", Mathf.Log10(sliderVolumen) * 20);
    }
}
