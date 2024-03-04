using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : RocketComponent
{

    public float force = 4;

    // Should also contain stuff like fuel usage

    private Rigidbody rocketRb;

    void Update()
    {
        // Addforce and stuff
        rootRigidbody.AddForceAtPosition(Vector3.up * force, transform.position);

    }

}
