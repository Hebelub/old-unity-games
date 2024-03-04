using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Camera camera;
    public float scrollSensitivity;
    public float minScroll = 0.2f;
    public float maxScroll = 100f;
    public float arrowMoveSpeed = 1f;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        ArrowMovement();
        Zoom();
    }

    public void ArrowMovement()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * arrowMoveSpeed * camera.orthographicSize, Input.GetAxis("Vertical") * arrowMoveSpeed * camera.orthographicSize, 0f);
    }

    public void Zoom()
    {
        Vector2 scroll = Input.mouseScrollDelta;
        if (scroll != Vector2.zero)
        {
            camera.orthographicSize -= scroll.y * scrollSensitivity * camera.orthographicSize;

            if (camera.orthographicSize > maxScroll) camera.orthographicSize = maxScroll;
            else if (camera.orthographicSize < minScroll) camera.orthographicSize = minScroll;
        }
    }

}
