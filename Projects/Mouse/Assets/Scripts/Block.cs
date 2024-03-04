using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckMovement());
	}
	
    private IEnumerator CheckMovement()
    {
        yield return null;

        while (rb.velocity.magnitude != 0)
        {
            yield return null;
        }
        rb.isKinematic = true;
    }
}
