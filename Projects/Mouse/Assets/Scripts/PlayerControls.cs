using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    private Rigidbody rb;

    public float maxSpeed = 4;

    public float speedNow;

    public Vector3 wantedSize;

    public float resizeFrames;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speedNow;
        if (moveInput.magnitude > 0)
        {
            rb.MovePosition(transform.position + moveInput);
        }

	}

    public void ResizePlayer(Vector3 newSize)
    {
        wantedSize = newSize;
        StartCoroutine(Resize());
    }

    private IEnumerator Resize()
    {
        Vector3 currentSize = transform.localScale;
        Vector3 difference = wantedSize - transform.localScale;
        Vector3 changePerFrame = difference / resizeFrames;
        for (int i = 0; i < resizeFrames; i++)
        {
            currentSize += changePerFrame;
            transform.localScale = currentSize;
            speedNow = maxSpeed / Mathf.Pow(currentSize.x * currentSize.z, 1f/5f);
            yield return null;

        }
    }

}
