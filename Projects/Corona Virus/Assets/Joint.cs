using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    private void Start()
    {
        // Create child
       // Instantiate(GameManager.GetRandomObject(), transform.position, Quaternion.identity, transform);
        // Center child
        transform.GetChild(1).transform.position = transform.position;
    }

}
