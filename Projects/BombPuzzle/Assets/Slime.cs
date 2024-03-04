using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Entity"))
        {
            if (collision.collider.GetComponent<Slime>() != null)
            {
                Debug.Log("You Won The Game");
            }

            collision.collider.transform.SetParent(transform);

            Destroy(collision.collider.GetComponent<Rigidbody2D>());
        }
    }

}
