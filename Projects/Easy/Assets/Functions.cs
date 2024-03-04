using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour {

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    public void InvertKinematic()
    {
        // rb.isKinematic = !rb.isKinematic;
        rb.AddForce(Vector3.up * 300f);
    }
}
