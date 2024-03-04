using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    public KeyCode boost;
    public KeyCode rotationRight;
    public KeyCode rotationLeft;

    public KeyCode restart;

    public Rigidbody rb;

    public float stopRotationSpeed;

    public float trust;
    public float rotationForce;

    public float zRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(restart))
        {
            Restart();
        }

        zRotation = transform.rotation.z;
        if(zRotation < 0.5f)
        {
            zRotation *= 2;
        }
        else if (zRotation >= 180)
        {
            zRotation = 1 - zRotation;
            zRotation *= -2;
        }

        if (Input.GetKey(boost))
        {
            Boost(trust * (1 - Mathf.Abs(zRotation)));
            float t = zRotation;
            if (t > 0.5f)
                t = 0.5f;
            Turn(-t * rotationForce);

            //if(Mathf.Abs(zRotation) < 0.08f)
            //{
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * 0.9f);
            //}
        }
        if (Input.GetKey(rotationRight))
        {
            Turn(-rotationForce);
        }
        if (Input.GetKey(rotationLeft))
        {
            Turn(rotationForce);
        }
    }

    public void Boost(float trust)
    {
        rb.AddRelativeForce(new Vector3(0, trust, 0));
        rb.angularVelocity = new Vector3(rb.angularVelocity.x * stopRotationSpeed, rb.angularVelocity.y * stopRotationSpeed, rb.angularVelocity.z * stopRotationSpeed);
    }

    public void Turn(float rotationForce)
    {
        rb.AddTorque(new Vector3(0, 0, rotationForce));
    }

    public void Restart()
    {
        Debug.Log("A");
        transform.position = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

}
