using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractToMouse : MonoBehaviour {

    public float attractiveness = 0f;

    public Vector3 mousePosition;

    public Rigidbody2D rb;

    public float divider;
    
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        int m = 1;
        if (Input.GetMouseButton(0))
        {
            m = -1;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;

        divider = direction.magnitude;
        if (divider < 1.5f) divider = 1;

        rb.AddForce( direction/direction.magnitude * attractiveness / divider * m );

	}
}
