using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void CalculateForce(Particle p1, Particle p2)
    {
        float distance = (p1.transform.position - p2.transform.position).magnitude;

        Vector3 direction = p1.transform.position - p2.transform.position;

        p1.rb.AddForce(direction * G*m1*m2/distance);
    }

}
