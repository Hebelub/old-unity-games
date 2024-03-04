using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float turnSpeed = 40f;

    public KeyCode forward;
    public KeyCode backwards;
    public KeyCode turnRight;
    public KeyCode turnLeft;
    public KeyCode up;
    public KeyCode down;

    public KeyCode bigger;
    public KeyCode smaller;

    void Update()
    {
        if (Input.GetKey(forward))
        {
            Move(1);
        }
        if (Input.GetKey(backwards))
        {
            Move(-1);
        }

        if (Input.GetKey(turnRight))
        {
            TurnPlayer(1);
        }
        if (Input.GetKey(turnLeft))
        {
            TurnPlayer(-1);
        }
        if (Input.GetKey(up))
        {
            Fly(1);
        }
        if (Input.GetKey(down))
        {
            Fly(-1);
        }

        if (Input.GetKey(bigger))
        {
            ChangeScale(moveSpeed / 75);
        }
        if (Input.GetKey(smaller))
        {
            ChangeScale(-moveSpeed / 75);
        }

    }

    public void ChangeScale(float change)
    {
        transform.localScale = transform.localScale + transform.localScale.normalized * change * GameManager.instance.speedOfTime;
    }

    public void Move(float direction)
    {
        transform.Translate(Vector3.forward * direction * moveSpeed * Time.deltaTime * GameManager.instance.speedOfTime);
    }
    public void Fly(float direction)
    {
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime * GameManager.instance.speedOfTime);
    }
    public void TurnPlayer(float direction)
    {
        transform.Rotate(Vector3.up * turnSpeed * direction * Time.deltaTime * GameManager.instance.speedOfTime);
    }

}
