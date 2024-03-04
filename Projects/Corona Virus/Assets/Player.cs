using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;

    public float sensitivity = 10f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) transform.Translate(Vector3.forward * speed);
        else if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * speed);
        else if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * speed);
        else if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * speed);
        else if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * speed * 400f);


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);

        if (collision.gameObject.CompareTag("Virus"))
        {
            Destroy(collision.gameObject);

        }
    }

    private void Update()
    {
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");

        //yaw = Mathf.Clamp(yaw, -90f, 90f);
        ////the rotation range
        //pitch = Mathf.Clamp(pitch, -60f, 90f);
        ////the rotation range

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

}
