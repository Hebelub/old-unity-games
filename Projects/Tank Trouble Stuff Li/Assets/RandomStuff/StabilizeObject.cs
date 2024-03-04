using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizeObject : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
