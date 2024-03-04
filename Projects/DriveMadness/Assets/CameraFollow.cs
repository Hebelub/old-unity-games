using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private Vector3 offset;

	void Start () {
        offset = - target.position + transform.position;
	}
	
	void LateUpdate () {
        transform.LookAt(target);

        transform.position = offset + target.position;
	}
}
