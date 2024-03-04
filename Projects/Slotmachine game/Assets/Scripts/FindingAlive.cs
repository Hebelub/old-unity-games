using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingAlive : MonoBehaviour
{

    public Rigidbody2D rb;

    public float popp = 1000f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        RandomDirection();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        transform.position = Vector3.zero;
    //        rb.velocity = Vector3.zero;
    //        RandomDirection();
    //    }
    //}

    public void Activate(Finding f)
    {
        SetSprite(f);
        RandomDirection();
        Destroy(gameObject, 1f);
    }

    public void SetSprite(Finding f)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = f.sprite;
    }

    public void RandomDirection()
    {
        Vector2 force = new Vector2 (Random.value * 2f - 1, Random.value * 1f + 0.25f).normalized * popp;

        rb.AddForce(force);
    }

}
