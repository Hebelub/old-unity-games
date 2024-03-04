using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public Color playerColor;

    public KeyCode right;
    public KeyCode left;
    public KeyCode forward;
    public KeyCode power;

    public float moveSpeed;
    public float jumpHeight;
    public float turnSpeed;

    private MeshRenderer mr;
    public Rigidbody rb;

    public float maxPower = 1f;
    public float rechargeSpeed;
    public float powerLeft;

    public float wealth = 0;

    private Color energyColorMax = new Color(1f, 1f, 1f) * 1.4f;
    private Color energyColorMin = new Color(0f, 0f, 0f); // Originally #205990

    public Transform spawnpoint;

    void Start() {

        GameManager.instance.players.Add(gameObject);

        mr = transform.GetChild(0).GetComponent<MeshRenderer>();
        mr.materials[0].color = playerColor;
        rb = GetComponent<Rigidbody>();
    }

    public Vector3 moveForce;
    public Vector3 turnForce;

    void Update() {

        powerLeft += rechargeSpeed * Time.deltaTime;
        if (powerLeft > maxPower)
        {
            powerLeft = maxPower;
        }

        moveForce = Vector3.zero;
        turnForce = Vector3.zero;

        if (Input.GetKey(right))
        {
            turnForce += transform.up * turnSpeed;
        }
        if (Input.GetKey(left))
        {
            turnForce += -transform.up * turnSpeed;
        }
        if (Input.GetKey(forward))
        {
            moveForce += transform.forward * moveSpeed;
        }

        downClick = Input.GetKeyDown(power);
        stayClick = Input.GetKey(power);
        upClick = Input.GetKeyUp(power);

        if (downClick)
        {
            Power();
        }
        else if (stayClick)
        {
            Power();
        }
        else if (upClick)
        {
            Power();
        }

        rb.AddTorque(turnForce);
        rb.AddForce(moveForce);

        // Calcurate brightness
        {
            float fraction = powerLeft / maxPower;

            Color difference = energyColorMax - energyColorMin;

            Color addColor = fraction * difference;

            mr.materials[1].color = energyColorMin + addColor;
        }

        //Debug.Log(powerUp);

    }

    public void Restart()
    {
        transform.position = spawnpoint.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = spawnpoint.rotation;

        powerLeft = maxPower;
    }

    public Power powerUp;

    //// Actions
    //delegate void PowerDown();
    //PowerDown powerDown;
    //delegate void PowerStay();
    //PowerStay powerStay;
    //delegate void PowerUp();
    //PowerUp powerDrop;

    public bool downClick;
    public bool stayClick;
    public bool upClick;

    // #region PowerUps

    public void Power()
    {
        //if (!hasEffect)
        //{
        powerUp.Effect(this);
        //}

        //switch (powerUp.powerUp) 
        //{
        //    case "Jump":
        //        Jump();
        //        break;
        //    case "Shoot":
        //        Shoot();
        //        break;
        //    case "Boost":
        //        Boost();
        //        break;
        //    case "Aim":
        //        Aim();
        //        break;
        //    case "Charge":
        //        Charge();
        //        break;
        //    default:
        //        None();
        //        break;
        //}
    }

    //private void Jump()
    //{
    //    if(downClick)
    //    {
    //        float powerUseage = 0.25f;
    //        if (energyLeft > powerUseage)
    //        {
    //            moveForce += Vector3.up * jumpHeight;
    //            energyLeft -= powerUseage;
    //        }
    //    }
    //}
    //private void Shoot()
    //{

    //}
    //private void Boost()
    //{
    //    if(stayClick)
    //    {
    //        float powerUseage = 0.05f;
    //        if (energyLeft > powerUseage)
    //        {
    //            moveForce += transform.forward * moveSpeed * 2;
    //            energyLeft -= powerUseage;
    //        }
    //    }
    //}
    //private void Aim()
    //{

    //}
    //private void Gain()
    //{

    //}
    //private void Charge()
    //{

    //}
    //private void None()
    //{
    //    Debug.LogWarning("No Power with that name");


    //}

    //#endregion


}
