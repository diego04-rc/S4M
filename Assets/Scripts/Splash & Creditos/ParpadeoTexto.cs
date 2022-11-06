using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParpadeoTexto : MonoBehaviour
{
    public Color color1, color2;
    public TextMeshProUGUI texto;

    private void Start()
    {
        texto = GameObject.Find("Texto").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        Parpadear();
    }

    public void Parpadear()
    {
        texto.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
    }
}
