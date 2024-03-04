using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInLine : MonoBehaviour
{
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.left * 5;
    }

    private void Update()
    {
        if(transform.position.x < -10)
        {
            transform.position = Vector3.right * 10 + transform.position.y * Vector3.up;
        }
    }
}
