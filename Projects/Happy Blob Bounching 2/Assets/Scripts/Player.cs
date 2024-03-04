using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode jump;

    // rb
    public Rigidbody2D rb;

    // Variables
    public float maxJumpVelocity = 100f;
    public float minJumpVelocity = 2f;
    public float chargeSpeed = 1f;
    public float currentFraction = 0f;

    // Flags
    public bool isJumping = false;
    public bool isCharging = false;

    public Vector2 angle = new Vector2(1f, 1f);

    public SpriteRenderer sr;

    public Gradient colorWhenCharging;

    private void Start()
    {
        angle = angle.normalized;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(jump) && isJumping == false)
        {
            currentFraction = Mathf.MoveTowards(currentFraction, 1, chargeSpeed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(jump))
        {
            Jump(currentFraction * maxJumpVelocity);
        }

        sr.color = colorWhenCharging.Evaluate(currentFraction);

    }

    public void Jump(float velocity)
    {
        currentFraction = 0f;

        if (velocity < minJumpVelocity)
        {
            // Do something else, the blob could for example blob itselves or something like that
        }
        else
        {
            // isJumping = true;
            rb.velocity = angle * velocity;
            angle.x = angle.x * -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Land();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
        currentFraction = 0f;

        Goal goal = collision.gameObject.GetComponent<Goal>();

        if (goal != null)
        {
            goal.NewPosition();
        }

    }

    public void Land()
    {
        // Must somehow detect when it lands. Maby by using hitboxes or something

        isJumping = false;
    }

}
