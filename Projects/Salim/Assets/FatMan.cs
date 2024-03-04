using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatMan : MonoBehaviour
{
    public float speed;

    public Transform camPos;

    private void Start()
    {
        
    }

    private void Update()
    {
        float xMov = - Input.GetAxis("Horizontal");
        float zMov = - Input.GetAxis("Vertical");

        transform.position += (transform.right * xMov + transform.forward * zMov) * speed;
    }

}
