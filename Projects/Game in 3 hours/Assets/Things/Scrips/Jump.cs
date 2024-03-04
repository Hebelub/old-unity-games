using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Item {

    public float jumpForce;

    private void OnMouseDown()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);


    }

}
