using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : Entity {

    public override void OnClick()
    {
        Destroy(gameObject); // Should also do a pop animation
    }

}

