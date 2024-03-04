using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber = 0;

    public KeyCode up = KeyCode.UpArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode right = KeyCode.RightArrow;

    public List<Transform> deathButtons;

    public float speed = .05F;

    public Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
    }

    public void Restart()
    {
        transform.position = spawnPosition;
    }

    private void Update()
    {
        if(IsDead())
        {

        }
        else
        {
            Move();   
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

    }

    public void Move()
    {
        Vector3 moveDirection = Vector3.zero;
        if(Input.GetKey(up))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(left))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(down))
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(right))
        {
            moveDirection += Vector3.right;
        }

        moveDirection = moveDirection.normalized;
        transform.position += moveDirection * speed;

    }

    public bool IsDead()
    {
        foreach (Transform transform in deathButtons)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
            foreach (Collider2D collider in colliders)
            {
                Player player = collider.transform.root.GetComponent<Player>();
                Debug.Log(player);
                if (player != null && player.playerNumber != playerNumber)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
