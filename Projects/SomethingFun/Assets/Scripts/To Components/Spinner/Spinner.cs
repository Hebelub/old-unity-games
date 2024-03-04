using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : RocketComponent
{

    public bool stabilization;

    public float rotateSpeed = 3f;
    public float powerUsage = 0.1f; // pr second when used

    public float[] savedRotations; 

    private Vector3 target;

    void Start ()
    {
        target = transform.forward;

    }

    void Update ()
    {

        // Should rotate itselves
        if (Input.GetKey(input[0]))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime * 20, 0, Space.Self);
            target = transform.forward;
            rootVehicle.CalculateCenterOfMass();
        }
        if (Input.GetKey(input[1]))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime * -20, 0, Space.Self);
            target = transform.forward;
            rootVehicle.CalculateCenterOfMass();
        }

        if (stabilization)
        {
            Vector3 relativePos = target; // - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos);
        }
        
    }

    private void LateUpdate()
    {
        // transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up);
       
    }

}
