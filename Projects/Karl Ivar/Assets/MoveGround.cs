using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {

    public float top;
    public float bottom;

    public float moveSpeed;

    private int direction = 1;

	void Start () {

	}
	
	void FixedUpdate () {
        if (transform.position.y > top)
        {
            direction = -1;
        } else if (transform.position.y < bottom)
        {
            direction = 1;
        }

        Move(Vector3.up * direction);
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed;
    }

}
