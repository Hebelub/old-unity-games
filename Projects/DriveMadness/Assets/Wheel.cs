using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    Transform car;
    Rigidbody carRb;

    public float force;

    public float sidewaysFriction = 0.8f;

    private void Start()
    {
        car = transform.root;
        carRb = car.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        float vertical = Input.GetAxis("Vertical");

        carRb.AddForceAtPosition(car.forward * force * vertical, transform.position);

        float multiplyer = -carRb.velocity.x;

        carRb.AddForceAtPosition(car.right * sidewaysFriction * multiplyer, transform.position);
    }

}
