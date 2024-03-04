using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public KeyCode up;
    public KeyCode left;
    public KeyCode down;
    public KeyCode right;

    public Vector3 wantedPosition;

    public float movementSpeed = 1f;
    //public float jumpForce = 100f;

    public Transform dir;

    Rigidbody rb;

    private void Start()
    {
        wantedPosition = transform.position;

        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(left))
        {
            Move(dir.right * -1);
        }
        if (Input.GetKeyDown(right))
        {
            Move(dir.right * 1);
        }
        if (Input.GetKeyDown(up))
        {
            Move(dir.up * 1);
        }
        if (Input.GetKeyDown(down))
        {
            Move(dir.up * -1);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            dir.Rotate(90, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir.Rotate(0, 90, 0);
        }

        transform.position = Vector3.MoveTowards(transform.position, wantedPosition, movementSpeed * Time.deltaTime);

    }

    public void Move(Vector3 direction)
    {
        wantedPosition += direction;

        //if (Bounds.Contains(wantedPosition))
        //{
        //    print("point is inside collider");
        //}

    }

    //public void Jump(float force)
    //{
    //    rb.AddForce(Vector3.up * force);
    //}

}
