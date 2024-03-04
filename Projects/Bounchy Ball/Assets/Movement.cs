using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    Vector3 direction;

    public float speed;

	void Start ()
    {
		
	}
	
	void Update ()
    {

        //direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        //transform.position += direction * speed;

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = transform.position.z;
        newPosition.y = transform.position.y;
        
        transform.position = newPosition;

    }
}
