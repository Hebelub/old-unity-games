using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    public float diggingSpeed = 0.1f;

    public bool isDigging = false;

    public void Digg(Block block)
    {
        isDigging = true;
        // block.Dig(this);
    }
}
