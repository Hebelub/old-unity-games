using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs : MonoBehaviour {

    public Vector2Int gridCoords;

    private void Start()
    {
        // Finding the coords of the object
        gridCoords = GameManager.instance.PositionToCoord(transform.position);
    }

}
