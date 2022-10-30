using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToInventoryCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject camaraInventario;
    private bool inventarioActivo;

    private void Awake()
    {
        camaraInventario = GameObject.Find("VCamInventario");
        camaraInventario.SetActive(false);
        inventarioActivo = false;
    }
    private void Start()
    {
        /*camaraNormal = GameObject.Find("ControladorDeCamara");
        camaraInventario = GameObject.Find("VCamInventario");
        mainCamera = GameObject.Find("Main Camera");
        inventarioActivo = false;*/

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            changeCamera();
        }
    }

    private void changeCamera()
    {
        if (!inventarioActivo)
        {
            camaraInventario.SetActive(true);
            inventarioActivo = true;
        }
        else
        {
            camaraInventario.SetActive(false);
            inventarioActivo = false;
        }
    }
}
