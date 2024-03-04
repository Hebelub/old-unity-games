using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Controlls
    // Forward / shoot when having ball:
    public KeyCode forward;
    // Right:
    public KeyCode left;
    // Backwards:
    public KeyCode backwards;
    // Left:
    public KeyCode right;

    // Speeds
    public float movementSpeed;
    public float rotationSpeed;
    public float maxShootPower;
    public float minShootPower;
    public float chargedShootPower;
    public float chargeSpeed = 0.1f;
    public float currentChargePortion = 0f;

    // Flags
    public bool prepearingShot = false;
    public bool hasBall = false;
    public bool isBacking = false;
    public bool turnLeft = false;
    public bool turnRight = false;
    public float turningSpeed = 0f;

    bool createdChargePortionLast = false;

    // Team
    public int team;

    // Start spawn position
    public Vector3 startPosition;
    public Quaternion startRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public GameObject[] objectsAttachedToPlayer;

    public Rigidbody2D rb;

    private void Update()
    {
        Movement();
    }

    bool isTouchingPlayer = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player.team != team)
            {
                isTouchingPlayer = true;
                /*player.*/UpdateArrowStuff();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.team != team)
            {
                isTouchingPlayer = false;
                /*player.*/UpdateArrowStuff();
            }
        }
    }

    bool hasX;

    public void Movement()
    {
        float t = Time.deltaTime;

        float rotationFloat = 1 - currentChargePortion;

        float speedFloat = 1f;
        if(isTouchingPlayer && hasBall)
        {
            speedFloat = 0f;
        }

        //else if (isTouchingPlayer)
        //{
        //    rotationFloat *= 0.5f;
        //}

        if(Input.GetKeyUp(forward) && hasBall && prepearingShot)
        {
            HasBall(false);

            ShootBall();

        }
        
        if(Input.GetKey(forward) && !hasBall && !prepearingShot)
        {
            if(!hasBall)
            {
                transform.Translate(Vector3.up * movementSpeed * t * speedFloat);
            }
        }
        else if(Input.GetKeyUp(forward))
        {
            chargedShootPower = minShootPower;
            prepearingShot = false;
        }
        else if(Input.GetKeyDown(forward))
        {
            prepearingShot = true;
            //chargedShootPower += chargeSpeed * t;
            //if (chargedShootPower > maxShootPower)
            //    chargedShootPower = maxShootPower;
        }
        if(Input.GetKey(forward) && hasBall && prepearingShot)
        {
            chargedShootPower += chargeSpeed * t;
            if (chargedShootPower > maxShootPower)
                chargedShootPower = maxShootPower;

            createdChargePortionLast = true;

            CreateChargePortion();
        }
        else if(createdChargePortionLast /*&& hasBall*/)
        {
            CreateChargePortion();
            createdChargePortionLast = false;
        }

        isBacking = false;
        if (Input.GetKey(backwards))
        {
            transform.Translate(-Vector3.up * movementSpeed / 2 * t * speedFloat);

            isBacking = true;
        }
        if (Input.GetKey(left) || turnLeft)
        {
            Turn(rotationFloat * 1 * t);
        }
        if (Input.GetKey(right) || turnRight)
        {
            Turn(rotationFloat * -1 * t);
        }
    }

    public void UpdateArrowStuff()
    {
        if (hasBall && isTouchingPlayer)
        {
            ActivateObject(1);
        }
        else if (hasBall)
        {
            ActivateObject(0);
        }
        else
        {
            DeactivateAll();
        }

    }

    public void CreateChargePortion()
    {
        float difference = maxShootPower - minShootPower;
        currentChargePortion = (chargedShootPower - minShootPower) / difference;
    }

    public void ShootBall()
    {

        Ball ball = GetComponentInChildren<Ball>();

        ball.attachedTo = null;

        ball.rb.bodyType = RigidbodyType2D.Dynamic;

        ball.transform.SetParent(GameManager.instance.ballsFolder);

        Vector3 direction = ball.transform.position - transform.position;

        ball.rb.AddForce(direction * chargedShootPower);

    }

    public void HasBall(bool isHolding)
    {
        hasBall = isHolding;

        UpdateArrowStuff();

        //// objectsAttachedToPlayer.SetActive(isHolding);
        //if(hasBall)
        //{
        //    ActivateObject(0);
        //}
        //else
        //{
        //    DeactivateAll();
        //}
    }

    public void ActivateObject(int index)
    {
        GameObject goActivate = objectsAttachedToPlayer[index];

        DeactivateAll();

        //foreach(GameObject disable in objectsAttachedToPlayer)
        //{
        //    disable.SetActive(false);
        //}

        goActivate.SetActive(true);
    }
    public void DeactivateAll()
    {
        foreach (GameObject disable in objectsAttachedToPlayer)
        {
            disable.SetActive(false);
        }
    }

    public void ReverseCurrentRotation()
    {
        Turn(-turningSpeed);

        //if (turningSpeed < 0)
        //{
        //    turnRight = true;
        //}
        //else if (turningSpeed > 0)
        //{
        //    turnLeft = true;
        //}
    }

    public void Turn(float ammount)
    {
        transform.Rotate(Vector3.forward * ammount * rotationSpeed);
        turningSpeed = ammount * rotationSpeed;
    }

    public void Restart()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}
