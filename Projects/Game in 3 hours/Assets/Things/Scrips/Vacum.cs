using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacum : Item
{

    public float radius;
    public float vacumForce;

    private void OnMouseDown()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in cols)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (col.gameObject == gameObject)
            {
                rb.AddExplosionForce(vacumForce / 8, transform.position, radius);

            }
            else if (col.GetComponent<Rigidbody>() != null)
            {


                float distance = (col.transform.position - transform.position).magnitude;

                if (distance < 1)
                {
                    distance = 1;
                }

                rb.AddExplosionForce(vacumForce / distance, transform.position, radius);

            }
        }
    }
}