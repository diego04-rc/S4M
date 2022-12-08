using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewTienda : MonoBehaviour
{
    public Transform viewMenuTienda;
    public float transitionSpeed;
    Transform currentView;

    InteraccionTenderoNPC interaccionTenderoNPC;
    // Start is called before the first frame update
    void Start()
    {
        interaccionTenderoNPC = FindObjectOfType<InteraccionTenderoNPC>();
        currentView = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (interaccionTenderoNPC.moveCameraToMenuTienda == true) {
            print("Cambiando camara");
            currentView = viewMenuTienda;
            interaccionTenderoNPC.moveCameraToMenuTienda = false;
        }
    }

    private void LateUpdate()
    {
        print("Cambiando camara p2");
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
        Vector3 currentAngle = new Vector3(
            Mathf.Lerp(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.Lerp(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.Lerp(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
        );
        transform.eulerAngles = currentAngle;
    }
    public void resetView() {
        transform.position = Vector3.Lerp(transform.position, viewMenuTienda.position, Time.deltaTime * transitionSpeed);
        print("CAMARA A POSICION NORMAL");
    }
}
