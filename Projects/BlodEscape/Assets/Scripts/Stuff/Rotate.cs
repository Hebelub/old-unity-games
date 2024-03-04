using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float rotationSpeed;

    public float minSpeed;
    public float maxSpeed;

    private void Start()
    {
        RandomRotation();

        StartCoroutine(IRotate());
    }

    public void RandomRotation()
    {
        rotationSpeed = Random.Range(minSpeed, maxSpeed);

        if(Random.Range(0, 2) == 0)
        {
            rotationSpeed *= -1;
        }
    }

    public IEnumerator IRotate()
    {
        while (true)
        {
            transform.Rotate(0, 0, rotationSpeed);

            yield return null;
        }
    }

}
