using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSteering : MonoBehaviour
{
    public float rotationSpeed = 10.0F;
    public float movementSpeed = 0.2F;
    public float backingSpeedMuliplyer = 0.75F;

    public float JumpForce = 100.0F;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Rotate(horizontal);
        MoveForward(vertical);

        if (Input.GetKey(KeyCode.Space) && IsGrounded() && rb.velocity.y <= 0)
        {
            Jump();
        }
    }

    public void Rotate(float ammount)
    {
        transform.Rotate(transform.up * ammount * rotationSpeed);
    }
    public void MoveForward(float ammount)
    {
        float magnitude = ammount < 0 ? backingSpeedMuliplyer : 1.0F; 

        transform.Translate(Vector3.left * ammount * movementSpeed * magnitude);
    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * JumpForce);
    }

    public bool IsGrounded()
    {
        return transform.position.y < 0.3F;
    }
}
