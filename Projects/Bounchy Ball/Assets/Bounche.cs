using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounche : MonoBehaviour {

    public float force = 500f;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    Vector3 direction = (collision.transform.position - transform.position).normalized;

    //    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        Vector3 direction = (collision.transform.position - transform.position).normalized;

        Debug.Log(direction * force);

        Rigidbody2D rb = collision.transform.parent.gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;

        rb.AddForce(direction * force);
    }
}
