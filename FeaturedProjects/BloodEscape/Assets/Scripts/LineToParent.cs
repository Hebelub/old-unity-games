using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToParent : MonoBehaviour {

    private Transform parent;
    private LineRenderer lr;

    void Start()
    {
        parent = transform.parent;
        lr = GetComponent<LineRenderer>();
	}
	
	void LateUpdate () {
        Vector3[] lines = { transform.position, parent.position};

        lr.SetPositions(lines);
    }
}
