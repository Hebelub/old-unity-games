using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour {

    public float force = 40f;
    public float sensitivity = 4f;

    private Rigidbody rb;

    float currentTurn;
    float currentDrive;

    public Transform[] wheels = new Transform[2];

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        // Get axis
        currentTurn = Input.GetAxis("Horizontal");
        currentDrive = Input.GetAxis("Vertical");

        // Move the vihecle
//        rb.velocity += currentDrive * force * transform.forward;
//        rb.AddRelativeForce(0, 0, force * currentDrive);
        transform.Rotate(0, sensitivity * currentTurn, 0);
	}
}
