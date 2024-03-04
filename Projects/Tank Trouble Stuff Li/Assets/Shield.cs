using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Tank owner;

    private void Update()
    {
        transform.position = owner.transform.position;

        // Physics.IgnoreCollision(owner.gameObject.);
    }

    

}
