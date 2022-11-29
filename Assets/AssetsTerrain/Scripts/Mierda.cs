using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mierda : MonoBehaviour
{

    public enum RotationAxes
    { // Movimiento ratón
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXandY;
    public float sensitivityHor = 9.0f; // velocidad
    public float sensitivityVert = 9.0f;
    public float minVert = -45.0f; // rango de rotación vertical
    public float maxVert = 45.0f;
    private float _rotationX = 0; // cabeceo (pitch) actual

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) body.freezeRotation = true; // impide que el motor de física rote la cámara

    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
            float rotationY = transform.localEulerAngles.y; // mantener el mismo ángulo de guiñada (yaw)
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }

    }
}
