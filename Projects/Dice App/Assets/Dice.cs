using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;

    public float force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    void Update()
    {
        // if (Random.value < 0.01f) rb.centerOfMass = transform.position + Vector3.right * 0.05f;

     //   rb.angularVelocity *= 2f * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
        }
    }

    public void Roll()
    {
        rb.AddForceAtPosition((Random.insideUnitSphere + Vector3.up * 3f).normalized * force, Random.insideUnitSphere * 1.0f);
    }

}
