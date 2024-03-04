using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnSpawn : MonoBehaviour
{

    Rigidbody2D rb;

    // public Transform moveTowards;
    public float moveForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 toPosition = Random.insideUnitCircle * 8; // moveTowards.position;

        rb.AddForce(((Vector2)transform.position - toPosition).normalized * -moveForce);
    }

    void Update()
    {

    }
}
