using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform lookAt;

    public float speed;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        Vector3 lookAtPosition = lookAt.position;

        Vector3 wantedPosition = lookAtPosition + offset;

        float multiplyer = 0f;
        multiplyer = (wantedPosition - transform.position).magnitude - 1.8f;
        if(multiplyer < 1f)
        {
            multiplyer = 1f;
        }

        transform.position = Vector3.MoveTowards(transform.position, wantedPosition, multiplyer * speed * Time.deltaTime);

    }

}
