using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    Rigidbody rb;

    public float movementForce = 1f;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject == GameManager.instance.virus)
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(this);
            
    //    } 
    //}

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(Movement());

    }

    IEnumerator Movement()
    {
        while (true)
        {
            rb.AddForceAtPosition(Random.insideUnitSphere * movementForce, Random.insideUnitSphere);

            yield return new WaitForSeconds(Random.value * 4 + 1.0f);
        }
    }

}
