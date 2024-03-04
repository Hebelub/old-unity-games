using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByFraction : MonoBehaviour
{
    private void Update()
    {
        transform.position = Vector3.up * (GameManager.instance.colectiveFraction - 0.5f) * 10f;
    }
}
