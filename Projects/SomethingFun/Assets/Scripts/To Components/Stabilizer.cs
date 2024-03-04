using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabilizer : MonoBehaviour {

    Quaternion oldRotation;

    public Transform spinning;

    public bool isSpinning = true;

    void Awake()
    {
        oldRotation = transform.rotation;
    }

    private void Update()
    {
        oldRotation = transform.rotation;

        if (isSpinning)
        {
            spinning.Rotate(0, 0, 20, Space.Self);
        }
    }

    void LateUpdate()
    {
        // Quaternion newRotation = transform.rotation;
        
        transform.rotation = oldRotation;
    }

}
