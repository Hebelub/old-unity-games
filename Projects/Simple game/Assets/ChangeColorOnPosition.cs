using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnPosition : MonoBehaviour
{
    private void Update()
    {
        Camera.main.backgroundColor = new Color((transform.position.x / 5) % 1, (transform.position.y / 5) % 1, (transform.rotation.z / 5) % 1);
        gameObject.GetComponent<MeshRenderer>().material.color = new Color((transform.position.x / 5 + 0.5f) % 1, (transform.position.y / 5 + 0.5f) % 1, (transform.rotation.z / 5 + 0.5f) % 1);
    }
}
