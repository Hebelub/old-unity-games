using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;

    public float speed = 0.02f;

	void Start () {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        anim.SetBool("isJumping", Input.GetKey(KeyCode.Space));

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed, 0));
        }


    }



}
