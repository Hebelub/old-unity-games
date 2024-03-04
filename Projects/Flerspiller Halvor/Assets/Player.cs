using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float growSpeed;

    public Vector3 scale;

    private void Update()
    {
        Grow();

        transform.localScale = scale;

    }

    public void Grow()
    {
        scale.y += growSpeed * Time.deltaTime;
    }

}
