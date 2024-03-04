using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform target;

    public float moveSpeed = 1f;
    public float stopDistance = 1.4f;

    //public float turnSpeed = 40f;

    //public KeyCode forward;
    //public KeyCode backwards;
    //public KeyCode turnRight;
    //public KeyCode turnLeft;

    void Start()
    {
        
    }

    void Update()
    {
        CloserToTarget(moveSpeed * Time.deltaTime * GameManager.instance.speedOfTime);
        
        //if (Input.GetKey(forward))
        //{
        //    Move(1);
        //}
        //if (Input.GetKey(backwards))
        //{
        //    Move(-1);
        //}

        //if (Input.GetKey(turnRight))
        //{
        //    TurnPlayer(1);
        //}
        //if (Input.GetKey(turnLeft))
        //{
        //    TurnPlayer(-1);
        //}
    }

    public void CloserToTarget(float distance)
    {
        Vector3 direction = transform.position - target.position;

        if(stopDistance < direction.magnitude)
        {
            direction = direction.normalized;
            direction = new Vector3(direction.z, 0, -direction.x);

            transform.Translate(direction * distance * GameManager.instance.speedOfTime);
        }
    }

    //public void Move(float direction)
    //{
    //    transform.Translate(transform.forward * direction * moveSpeed * Time.deltaTime);
    //}
    //public void TurnPlayer(float direction)
    //{
    //    transform.Rotate(Vector3.up * turnSpeed * direction * Time.deltaTime);
    //}

}
