using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOnWheel : MonoBehaviour
{
    public Camera cam;
    public float scrollSpeed = 5.0F;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        float wheeling = Input.mouseScrollDelta.y;

        Zoom(wheeling);
    }

    public void Zoom(float ammount)
    {
        cam.fieldOfView -= ammount * scrollSpeed;
    }
}
