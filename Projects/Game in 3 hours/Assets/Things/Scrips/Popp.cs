using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popp : Item {

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
