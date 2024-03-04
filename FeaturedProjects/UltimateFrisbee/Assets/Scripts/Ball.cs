using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Rigidbody2D rb;

    public Player attachedTo;

    // Collours
    public SpriteRenderer sr;
    public Gradient speedToColor;
    public float positionOnSpeedToColorGradient;
    public float wantedColor;

    // To fix a bug
    public float distanceFromPlayer = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ISpeedToColor());
    }

    public IEnumerator ISpeedToColor()
    {
        while(true)
        {
            if (attachedTo != null)
            {
                wantedColor = attachedTo.currentChargePortion;
            }
            else
            {
                // Then speed somehow should tell the collour of the ball
            }

            positionOnSpeedToColorGradient = Mathf.MoveTowards(positionOnSpeedToColorGradient, wantedColor /*rb.velocity.magnitude / 5f*/, 2.3f * Time.deltaTime);

            sr.color = speedToColor.Evaluate(positionOnSpeedToColorGradient);

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attachedTo != null)
            {
                // attachedTo.CreateChargePortion();
                attachedTo.chargedShootPower = 0f;
            }

            Detach();

            attachedTo = collision.transform.GetComponent<Player>();

            attachedTo.HasBall(true);

            transform.SetParent(collision.transform);

            rb.bodyType = RigidbodyType2D.Kinematic;

            if (rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
            }

            FixBallPosition();
            
        }
        //else if (collision.gameObject.CompareTag("Wall"))
        //{
        //    // Make attachedTo.speed.directionItIsTurning = 0f;

        //    attachedTo.StopCurrentRotation();
        //}
    }

    Vector3 lastPositionOfBall = Vector3.zero;

    private void LateUpdate()
    {
        lastPositionOfBall = transform.position;
    }

    //// This is to fix a bug where the ball is bounching before it is set to kinematic
    //public void SetBallToLastTickPosition()
    //{
    //    transform.position = lastPositionOfBall;
    //}

    public void FixBallPosition()
    {
        //Vector3 direction = transform.position - attachedTo.transform.position;

        //transform.position = direction * distanceFromPlayer + attachedTo.transform.position;

        //rb.velocity = Vector3.zero;

        //Debug.Log(transform.position);


    }

    public void Detach()
    {
        if (attachedTo != null)
        {
            attachedTo.HasBall(false);
            attachedTo = null;
            transform.SetParent(GameManager.instance.ballsFolder);
        }

    }

}
