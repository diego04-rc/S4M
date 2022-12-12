using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecogerChip : MonoBehaviour
{
    private int fragmentos = 0;
    public TextMeshProUGUI nFragmentos;
    GameObject panelFragmentos;
    // Start is called before the first frame update
    void Start()
    {
        nFragmentos.SetText(fragmentos.ToString());
        panelFragmentos = GameObject.Find("PanelFragmentos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chipRecogido()
    {
        if (fragmentos == 0) {
            panelFragmentos.SetActive(true);
        }
        fragmentos++;
        nFragmentos.SetText(fragmentos.ToString());
    }
}
