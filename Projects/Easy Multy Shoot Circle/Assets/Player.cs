using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.1f;    

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 movePos = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * moveSpeed * Time.deltaTime;

        transform.position += movePos;
    }
}
