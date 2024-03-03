using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log(collision);

            Ball ball = collision.gameObject.GetComponent<Ball>();    
            ball.attachedTo.ReverseCurrentRotation();
        }
    }
}
