using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100;

    Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Shoot();
    }

    public void Shoot()
    {
        Transform player = GameManager.instance.player.transform;
        rb.AddForce(speed * (transform.position - player.position), ForceMode.VelocityChange);
    }
}
