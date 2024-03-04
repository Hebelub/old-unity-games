using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    Rigidbody rb;

    public KeyCode forward;
    public KeyCode left;
    public KeyCode brake;
    public KeyCode right;

    public bool isBraking = false;
    public bool isTurning = false;

    public float currentSpeed = 0f;
    public float acceleration = 0.25f;

    public GameObject[] tires;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    private void OnTriggerStay(Collider other)
    {
        //if (other != null && !isBraking && isTurning) // if it is not itself
        //{
        //    float magnitude = rb.velocity.magnitude;

        //    rb.velocity = transform.right * magnitude;
        //}

        // The speed should ++ if you gass
        if (Input.GetKey(forward))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
    }

    public PhysicMaterial brakeMaterial;
    public PhysicMaterial noFrictionMaterial;

    void FixedUpdate ()
    {
        isTurning = false;

		if(Input.GetKey(forward) && rb.velocity.magnitude < 14f)
        {
            isTurning = true;
            rb.velocity += transform.right * 0.34f;
        }
        
        if(Input.GetKey(left))
        {
            isTurning = true;
            transform.Rotate(Vector3.up, -5f);
        }
        if (Input.GetKey(right))
        {
            transform.Rotate(Vector3.up, 5f);
        }

        if(Input.GetKeyDown(brake))
        {
            isBraking = true;

            foreach(GameObject tire in tires)
            {
                Collider col = tire.GetComponent<Collider>();

                col.material = brakeMaterial;

            }
        }
        else if (Input.GetKeyUp(brake))
        {
            isBraking = false;

            foreach (GameObject tire in tires)
            {
                Collider col = tire.GetComponent<Collider>();

                col.material = noFrictionMaterial;

            }
        }
    }
}
