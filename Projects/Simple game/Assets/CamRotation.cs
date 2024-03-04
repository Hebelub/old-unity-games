using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public Transform camCenter;
    public float rotationOnClick = 90f;
    public float rotationTime = 1f;

    private void Update()
    {
        camCenter.rotation = transform.rotation;
    }
    private void OnMouseDown()
    {
        float startRotation = transform.rotation.y;
        StartCoroutine(IRotate());

        IEnumerator IRotate()
        {
            float t = 0;

            while(true)
            {
                t += Time.deltaTime * rotationTime;
                if (t > 1) t = 1;

                transform.rotation = Quaternion.Euler(transform.rotation.x, startRotation + rotationOnClick * t, transform.rotation.z);
                yield return null;
            }

        }
    }

}
