using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Color color;

    public float shootForce = 20f;
    public Rigidbody rb;

    public Color[] possibleColors = new Color[] { Color.blue, Color.green, Color.yellow, Color.red };

    public bool setteled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Esamble();
    }

    public void Send()
    {
        rb.AddForce(shootForce * Vector3.forward);
    }

    public void Esamble()
    {
        ChangeColor(possibleColors[Random.Range(0, possibleColors.Length)]);

        void ChangeColor(Color color)
        {
            this.color = color;
            GetComponent<MeshRenderer>().material.color = color;
        }
    }

    int connections = 0;

    public void CheckParentUp()
    {
        Ball parentBall = GetComponentInParent<Ball>();
        if(parentBall != null)
        {
            if (parentBall.color == color)
            {
                parentBall.CheckParentUp();
            }
            else
            {
                  CheckParentDown(this);
            }
        }

    }
    public void CheckParentDown(Ball root)
    {
        foreach (Transform t in transform)
        {
            Debug.Log("A"); 
            root.connections += 1;

            Ball ball = t.GetComponent<Ball>();

            ball.CheckParentDown(root);
        }

        if (this == root && root.connections > 4)
        {
            Destroy(root.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!setteled)
        {
            setteled = false;
            Ball ball = collision.gameObject.GetComponent<Ball>();
            transform.SetParent(ball.transform);
            Destroy(rb);
            ball.rb.centerOfMass = ball.transform.position;
            Debug.Log("CA: " + color + "CB: " + ball.color);
            if (ball.color == color)
            {
                Destroy(ball.gameObject);
            }
            Shoot.instance.Activate();
            //CheckParentUp();
        }
    }
}
