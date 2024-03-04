using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMap : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;

    private void Start()
    {
        transform.position = offset;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 0.1f);
    }
}
