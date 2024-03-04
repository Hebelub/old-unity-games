using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArrow : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
}
