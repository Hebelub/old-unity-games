using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalansingObject : MonoBehaviour {

    public float mass;

    private void OnMouseDown()
    {
        DestroyThisGo();
    }

    public void DestroyThisGo()
    {
        Destroy(gameObject);
    }

}
