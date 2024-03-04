using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public int team;

    public SpriteRenderer sr;

    public Color colorWhenTouchingBall;
    public Color normalColor;

    private void Start()
    {
        normalColor = sr.color;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.parent.parent.CompareTag("Ball"))
        {
            Ball ball = collision.transform.parent.parent.GetComponent<Ball>();

            if (ball.attachedTo != null)
            {
                if(ball.attachedTo.team != team)
                {
                    GameManager.instance.Restart();
                    GameManager.instance.Score(team, 1);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.parent.CompareTag("Ball"))
        {
            sr.color = colorWhenTouchingBall;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.parent.CompareTag("Ball"))
        {
            sr.color = normalColor;
        }
    }

}
