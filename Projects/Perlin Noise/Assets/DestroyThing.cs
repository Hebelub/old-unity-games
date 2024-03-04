using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThing : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }
}
