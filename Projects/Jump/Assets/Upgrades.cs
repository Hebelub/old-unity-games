using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    // How hard you can hit the blocks
    public float toolSpeed = 0.25f;

    // How big the cubes can be in all directions
    public Vector3 maxCubeSize = Vector3.one;

    // The smallest size of the cubes in all directions
    public Vector3 minCubeSize = Vector3.one;

    // The number of bombs
    public int nrOfBombs = 1;

    public float bombDamage = 1.5F;
}
