using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWhenDamaged : MonoBehaviour {

    private Rigidbody2D rb;

    public float protection = 4;

    private Vector2 lastVelocity;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        lastVelocity = rb.velocity;
	}
	
	void Update () {

        float damage = (lastVelocity - rb.velocity).magnitude;

        if (damage > protection)
        {
            Destroy(gameObject);
        }

        // Get rb velocity
        lastVelocity = rb.velocity;

	}
}
