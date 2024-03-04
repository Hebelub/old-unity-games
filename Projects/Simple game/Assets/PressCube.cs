using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressCube : Level
{
    private void OnMouseDown()
    {
        ToggleChildren();
    }
}
