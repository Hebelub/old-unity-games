using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtFood : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        /*transform.rotation =*/ transform.LookAt(transform.root.GetComponent<Player>().target);
    }
}
