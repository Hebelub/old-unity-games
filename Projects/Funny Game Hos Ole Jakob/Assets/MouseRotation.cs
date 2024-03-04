using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{

    public float rotationSpeed = 5.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        // Screen.lockCursor = true;
    }

    public float sensitivity = 1;

    void FixedUpdate()
    {
        float rotateHorizontal = -Input.GetAxis("Mouse X");
        float rotateVertical = -Input.GetAxis("Mouse Y");
        transform.RotateAround(transform.position, -Vector3.up, rotateHorizontal * sensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
        transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity); // again, use transform.Rotate(transform.right * rotateVertical * sensitivity) if you don't want the camera to rotate around the player

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
        }

        Vector3 hRotation = Vector3.up * Input.GetAxisRaw("Horizontal"); 
        Vector3 vRotation = -Vector3.right * Input.GetAxisRaw("Vertical");

        transform.Rotate(hRotation, rotationSpeed);
        transform.Rotate(vRotation, rotationSpeed);
    }
}
