using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform objectToLookAt;

    private void Update()
    {
        transform.LookAt(objectToLookAt.position);
    }
}
