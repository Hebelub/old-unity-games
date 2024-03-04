using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForBuildScene : MonoBehaviour {

    Transform cameraTransform;

	// Use this for initialization
	void Start () {
        // Get the camera
        cameraTransform = transform.GetChild(0).GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {

        //// Makes the camera look at the center point
        //cameraTransform.LookAt(transform);

        // When you hit the left or right key, turn the camera around
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, -1.2f, 0, Space.World);
            CameraMoved();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 1.2f, 0, Space.World);
            CameraMoved();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cameraTransform.Translate(0, scroll, 0, Space.Self);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(1.2f, 0, 0);
            CameraMoved();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-1.2f, 0, 0);
            CameraMoved();
        }

        if (Input.GetKey(KeyCode.W))
        {
            cameraTransform.Rotate(-1.2f, 0, 0, Space.Self);
            CameraMoved();
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraTransform.Rotate(1.2f, 0, 0, Space.Self);
            CameraMoved();
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraTransform.Rotate(0, -1.2f, 0, Space.World);
            CameraMoved();
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraTransform.Rotate(0, 1.2f, 0, Space.World);
            CameraMoved();
        }

        if (Input.GetKey(KeyCode.PageDown))
        {
            cameraTransform.Translate(0, 0, -1.2f / 8, Space.Self);
            CameraMoved();
        }
        if (Input.GetKey(KeyCode.PageUp))
        {
            cameraTransform.Translate(0, 0, 1.2f / 8, Space.Self);
            CameraMoved();
        }

        // This script really needs a fix, it should also be possible to move camera with mouse!

    }

    private void CameraMoved()
    {
        Build.mustRaycastNextIteration = true;
    }
}
