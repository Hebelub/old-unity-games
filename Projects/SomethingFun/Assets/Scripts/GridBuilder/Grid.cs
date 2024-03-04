using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public float size = 1; // The space between each gridpoint

    [SerializeField]
    private GameObject gridCollider;

    // Is finding the closes point on the grid from given point, should
    public Vector3 PointToGrid(Vector3 hitPoint)
    {

        Vector3 gridPosition = new Vector3(Mathf.Round(hitPoint.x / size),
                                           Mathf.Round(hitPoint.y / size),
                                           Mathf.Round(hitPoint.z / size) 
                                           * size);

        return gridPosition;
    }

    public Vector3 PlaceOnGrid(Vector3 hitPoint)
    {
        Vector3 pointOnGrid = PointToGrid(hitPoint);

        PlaceGridCollider(pointOnGrid);

        return pointOnGrid;
    }

    public void PlaceGridCollider(Vector3 position)
    {

    }

}
