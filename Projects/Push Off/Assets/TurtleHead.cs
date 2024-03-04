using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleHead : MonoBehaviour {

    Rigidbody rb;

    float speed;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.transform.parent.CompareTag("Player") && false)
        {
            Debug.Log(speed + " " + transform.parent.gameObject.GetComponent<Player>().right);

            other.transform.parent.gameObject.GetComponent<Rigidbody>().AddExplosionForce(speed * 5f, transform.position - Vector3.up * (0.73f + 0.3f), 10f, speed * 0f);
        }

    }

    private void LateUpdate()
    {
        speed = rb.velocity.magnitude;
    }

}
