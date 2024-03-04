using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform golfBall;

    public float speed = 1f;

    private void Update()
    {
        Vector3 direction = golfBall.position - transform.position;

        if (direction.magnitude < speed)
        {
            transform.position = golfBall.position;
        }
        else
        {
            transform.position += direction.normalized * speed;
        }
    }
}
