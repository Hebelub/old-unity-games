using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float horizontal;
    public KeyCode jump;
    float turn;
    float turnVertical;
    float vertical;

    public float jumpForce = 100f;
    public float turnSensitivity = 10f;
    public float moveForwardSpeed = 1f;

    public Transform tiltTransform;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        // jump = Input.GetAxis("Jump");
        turn = Input.GetAxis("Turn");
        turnVertical = Input.GetAxis("TurnVertical");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(jump))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        // Lets turn the camera and stuff ._.
        transform.Rotate(Vector3.up * turn * turnSensitivity * Time.deltaTime);

        tiltTransform.Rotate(Vector3.right * turnVertical * turnSensitivity * Time.deltaTime / 5f);

        transform.Translate(Vector3.forward * vertical * moveForwardSpeed * Time.deltaTime);

    }

}
