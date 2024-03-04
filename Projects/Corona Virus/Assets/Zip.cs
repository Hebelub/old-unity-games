using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zip : MonoBehaviour
{
    public Transform start;
    public Transform end;

    public Joint joint;

    private bool dirRight = true;
    public float speed = 2.0f;

    private void Start()
    {
        transform.rotation = Random.rotation;
        speed = Random.value * 6;
    }

    void Update()
    {
        float jointDistance = (joint.transform.position - start.position).magnitude;

        if (dirRight)
            joint.transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            joint.transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (jointDistance >= (end.position - start.position).magnitude - 1)
        {
            dirRight = false;
        }

        if (jointDistance <= (start.position - start.position).magnitude + 1)
        {
            dirRight = true;
        }
    }
}
