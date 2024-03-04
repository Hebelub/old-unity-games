using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfRotator : MonoBehaviour
{
    public GolfBall golfBall;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * h * golfBall.rotationSpeed);
    }

}
