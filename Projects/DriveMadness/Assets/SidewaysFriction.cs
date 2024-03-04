using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysFriction : MonoBehaviour {

    Transform car;
    Rigidbody carRb;

    public Vector3 friction = new Vector3(1, 0, 0);

	void Start () {
        car = transform.root;
        carRb = car.GetComponent<Rigidbody>();
	}

    private void OnTriggerStay(Collision collision)
    {
        
        float x = carRb.velocity.x;
        float y = carRb.velocity.y;
        float z = carRb.velocity.z;

        if (x > friction.x)
            x -= friction.x;
        else x = 0;
        if (y > friction.y)
            y -= friction.y;
        else y = 0;
        if (z > friction.z)
            z -= friction.z;
        else z = 0;

        Vector3 newFriction = new Vector3(x, y, z);

        Debug.Log(newFriction);

        carRb.velocity -= newFriction;
    }

}
