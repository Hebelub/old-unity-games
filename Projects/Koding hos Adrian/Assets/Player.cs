using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode moveUp;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode moveDown;

    public float moveSpeed = 0.25f;

    private void OnCollisionEnter(Collision collision)
    {
        if(null != collision.gameObject.GetComponent<PickUp>())
        {
            PickUp p = collision.gameObject.GetComponent<PickUp>();

            switch (p.name)
            {
                case "growH":
                    transform.localScale = transform.localScale += Vector3.right / 4;
                    break;
                case "growV":
                    transform.localScale = transform.localScale += Vector3.forward / 4;
                    break;
                default:
                    break;
            }

            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKey(moveUp))
        {
            Move(Vector3.forward);
        }
        if (Input.GetKey(moveLeft))
        {
            Move(Vector3.left);
        }
        if (Input.GetKey(moveRight))
        {
            Move(Vector3.right);
        }
        if (Input.GetKey(moveDown))
        {
            Move(Vector3.back);
        }
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed;
    }
}
