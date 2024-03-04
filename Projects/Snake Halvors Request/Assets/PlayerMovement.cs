using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Singelton
    public static PlayerMovement instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;

    void Update()
    {
        var direction = Vector3.zero;

        if (Input.GetKeyDown(right))
        {
            direction += Vector3.right;
        }
        if (Input.GetKeyDown(left))
        {
            direction += Vector3.left;
        }
        if (Input.GetKeyDown(up))
        {
            direction += Vector3.up;
        }
        if (Input.GetKeyDown(down))
        {
            direction += Vector3.down;
        }

        Move(direction);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent.gameObject.GetComponent<Goal>() != null)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        GameObject thisLevel = LevelGenerator.instance.levelObjects[0];
        Destroy(thisLevel);
        LevelGenerator.instance.levelObjects.Remove(thisLevel);
        Move(Vector3.forward);
        LevelGenerator.instance.ActivateFirstLevel(true);
        LevelGenerator.instance.CreateLevel();
    }

    public void Move(Vector3 direction)
    {
        Collider[] col = Physics.OverlapSphere(transform.position + direction, 0.25f);
        if (col.Length > 0)
        {
            var c = col[0].gameObject;

            if (c.GetComponentInParent<Obstacle>() != null)
            {
                if (direction == Vector3.forward)
                {
                    Destroy(c);

                    transform.position += direction;

                }

                return;
            }

            if (c.GetComponentInParent<Stone>() != null)
            {
                c.GetComponentInParent<Stone>().Dig();
            }
            else if (c.GetComponentInParent<Portal>())
            {
                c.GetComponentInParent<Portal>().Teleport();
            }
            

        }

        transform.position += direction;
    }

}
