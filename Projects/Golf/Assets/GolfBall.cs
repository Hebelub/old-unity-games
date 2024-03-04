using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    public float maxForce;
    public float minForce;
    public float chargeSpeed;

    public float rotationSpeed;

    public float currentForce;

    private void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StartCoroutine(Charge());
        }

        IEnumerator Charge()
        {
            currentForce = minForce;

            while(!Input.GetKeyUp(KeyCode.Space))
            {
                yield return null;

                currentForce += chargeSpeed;
            }

            Shoot(currentForce);

        }

    }

    public void Shoot(float force)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * force);

    }

}
