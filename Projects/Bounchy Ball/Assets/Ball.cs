using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 startPosition;
    Rigidbody2D rb;
    public float speed;

	void Start () {

        startPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();

	}
	
	void Update () {

        //if (rb.velocity.magnitude != speed)
        //{
        //    rb.velocity = rb.velocity.normalized * speed;
        //}


        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            rb.velocity = Vector3.zero;
        }
	}
}
