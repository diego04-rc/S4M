using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecogerChip : MonoBehaviour
{
    private int fragmentos = 0;
    public TextMeshProUGUI nFragmentos;
    // Start is called before the first frame update
    void Start()
    {
        nFragmentos.SetText(fragmentos.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chipRecogido()
    {
        fragmentos++;
        nFragmentos.SetText(fragmentos.ToString());
    }
}
