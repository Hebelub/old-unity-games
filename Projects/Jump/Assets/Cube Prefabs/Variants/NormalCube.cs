using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCube : Cube
{
    private new void Start()
    {
        base.Start();
    }

    private void OnMouseDown()
    {
        Hit(upgrades.toolSpeed);
    }

}
