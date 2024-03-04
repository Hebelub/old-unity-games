using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlls : MonoBehaviour {

    // Variables for axis
    float horizontal;
    float vertical;

    bool engineOn;

    // The vehicle
    public float currentSpeed = 0f; // Vehicle should probably be a scriptable object
    public float sensitivity = 0.2f;
    public float acceleration = 0.05f;
    public float friction = 0.005f;
    public float topSpeed = 0.4f;
    // Drag
    public Vector3 drag = new Vector3(0.1f, 6f, 0f);

    // Rigidbody
    Rigidbody rb;

	void Start () {
        currentSpeed = topSpeed;
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        // Get axis
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        // Get engineOn
        engineOn = Input.GetKey(KeyCode.Space);
        
        // Vihecle turn
        Turn(horizontal * sensitivity);
        // Vihecle lift up/down
        Lift(-vertical * sensitivity);
        // Accelerate
      //  Accelerate(acceleration);
        // Move vihecle forward/backwards
        Forward(currentSpeed);

        // Drag
        Vector3 subtractor = Vector3.zero;
        if (rb.velocity.x > drag.x)
        {
            subtractor.x = drag.x;
        } else
        {
            subtractor.x = rb.velocity.x;
        }
        if (rb.velocity.y > drag.y)
        {
            subtractor.y = drag.y;
        } else
        {
            subtractor.y = drag.y;
        }
        if (rb.velocity.z > drag.z)
        {
            subtractor.z = drag.z;
        } else
        {
            subtractor.z = drag.z;
        }
        rb.velocity -= subtractor;
        rb.velocity = Vector3.zero;

    }

    private void Turn(float turn)
    {
        transform.Rotate(0, turn, 0);
    }

    private void Lift(float ammount)
    {
        transform.Rotate(ammount, 0, 0);
    }

    private void Forward(float distance)
    {
        transform.Translate(0, 0, distance);
    }

    private void Accelerate(float ammount)
    {
        if (engineOn)
           currentSpeed += ammount;
        if (currentSpeed > friction)
            currentSpeed -= friction;
        else
            currentSpeed = 0;
        if (currentSpeed > topSpeed)
            currentSpeed = topSpeed;
    }

}
