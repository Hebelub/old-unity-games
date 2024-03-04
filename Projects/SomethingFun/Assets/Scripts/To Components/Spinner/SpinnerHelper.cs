using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerHelper : MonoBehaviour {

    public KeyCode[] input;
    public float rotateSpeed;
    public float powerUsage;

    private Rocket rocketScript;

    private void Start()
    {
        rocketScript = transform.root.GetComponent<Rocket>();
    }

    private void OnTriggerStay(Collider other)
    {
        float t = Time.deltaTime;

        if (rocketScript.powerLeft * t > powerUsage * t)
        {
            if (Input.GetKey(input[0]) && !Input.GetKey(input[1]))
            {
                other.transform.RotateAround(transform.position, transform.up, rotateSpeed * t * 10);
                rocketScript.powerLeft -= powerUsage * t;
            }
            else if (Input.GetKey(input[1]))
            {
                other.transform.RotateAround(transform.position, transform.up, -rotateSpeed * t * 10);
                rocketScript.powerLeft -= powerUsage * t;
            }
        }
    }
}
