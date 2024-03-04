using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    KeyCode jump = KeyCode.Space;

    public float jumpForce = 100;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(jump) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce * rb.mass);
        }
    }

    private bool IsGrounded()
    {
        return transform.position.y < 0.25f;

    } 
}
