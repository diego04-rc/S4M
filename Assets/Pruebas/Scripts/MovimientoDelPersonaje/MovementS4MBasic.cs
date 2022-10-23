using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementS4MBasic : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 movementInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementInput.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementInput.z = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementInput.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movementInput.x = -1;
        }

        MoveBasic(movementInput);
    }

    void MoveBasic(Vector3 direction)
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
