using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVibration : MonoBehaviour
{
    // Start is called before the first frame update
    Cinemachine.CinemachineImpulseSource golpe;

    private void Awake()
    {
        golpe = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G)) Vibrar();
    }

    private void Vibrar()
    {
        golpe.GenerateImpulse();
    }
}
