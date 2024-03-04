using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour {

    public float power = 20;
    public float torque = 1;

    private Rigidbody rb;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        // Store inputs as variables
        float turn = Input.GetAxis("Horizontal");
        float force = Input.GetAxis("Vertical");
        // Addforce and stuff
        rb.AddForce(transform.up * force * power);
        // AddTorque...
        rb.AddTorque(transform.forward * turn * torque * -1);

    }
}
