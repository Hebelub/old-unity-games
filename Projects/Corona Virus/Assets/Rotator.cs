using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 10f;

    public Joint joint;

    private void Start()
    {
        transform.rotation = Random.rotation;
        joint.transform.position = transform.position + (Random.value * 6f + 1) * Vector3.right;
        rotationSpeed = Random.value * 3f;
    }

    private void FixedUpdate()
    {
        Rotate(rotationSpeed / (joint.transform.position - transform.position).magnitude);
    }

    public void Rotate(float speed)
    {
        transform.Rotate(Vector3.up * speed);
    }
}
