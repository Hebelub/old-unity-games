using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public float jumpHeight;
    public float brakeForce = 6f;

    public float wantedSpeed = 0.6f;

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();

        rb.angularVelocity = 200;
        rb.AddForce(new Vector2(300f, 0));
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            rb.angularVelocity *= -1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity *= -1;
        }
        //if (rb.velocity.magnitude < wantedSpeed)
        //{
        //    rb.velocity += new Vector2(0f, rb.velocity.y * 2f);
        //}
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        rb.AddForce(collision.transform.up * 200);
    //        Debug.Log("A");
    //    }
    //}

}
