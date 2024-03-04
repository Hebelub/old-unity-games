using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    bool isGrabbed; 

    private void OnMouseDown()
    {
        isGrabbed = true;
    }
    private void OnMouseUp()
    {
        isGrabbed = false;
    }
    private void Update()
    {
        if(isGrabbed)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         //   Vector3 grabPos = transform.position - worldPosition;
            transform.position = worldPosition + Vector3.forward * 10 /*+ grabPos*/;
        }
    }
}
