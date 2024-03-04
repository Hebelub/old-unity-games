using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public KeyCode forward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode back;

    public float movementSpeed = 10f;

    public float rotationSpeed = 90f;

    private void Update()
    {
        if (transform.position.y < -5f)
        {
            transform.position = Vector3.zero;
        }

        if (Input.GetKey(forward))
        {
            Move(transform.forward);
        }
        if (Input.GetKey(left))
        {
            Rotate(-1);
        }
        if (Input.GetKey(right))
        {
            Rotate(1);
        }
        if (Input.GetKey(back))
        {
            Move(-transform.forward);
        }

    }

    public void Move (Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    public void Rotate (float rotation)
    {
        transform.Rotate(Vector3.up * rotation * rotationSpeed * Time.deltaTime);
    }

}
