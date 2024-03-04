using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Rigidbody rb;

    public float u;

    public 

    void Start()
    {
        
    }

    void Update()
    {
        rb.AddRelativeForce(Vector3.up * u);
    }
}
