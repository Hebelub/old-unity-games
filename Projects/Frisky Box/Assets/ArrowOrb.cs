using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOrb : MonoBehaviour {

    public float moveSpeed = 6f;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKeyDown(KeyCode.Space) && collision.tag == "Orb")
        {
            Transform root = collision.transform.root;

            Rigidbody2D rb = root.GetComponent<Rigidbody2D>();

            rb.velocity = -transform.up * moveSpeed;

            Destroy(gameObject);

        }
    }

    //public Vector2 AngleToVector(float angle)
    //{
    //    angle *= Mathf.Deg2Rad;
    //    return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //}

}
