using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : RocketComponent {

    public float force = 20;

    // Should also contain stuff like fluel usage

    //private Rigidbody rocketRb;

    void Start()
    {
        //rocketRb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(input[0]))    
        {
            // Addforce and stuff
            rootRigidbody.AddForceAtPosition(transform.up * force, transform.position);
        }

    }

}
