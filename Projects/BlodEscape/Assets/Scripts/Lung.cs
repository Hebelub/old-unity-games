using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lung : MonoBehaviour {

    public GameObject lungHole;

    public Vector2Int lungGrid = new Vector2Int(12, 12);

    public float spacing;

    private void Start()
    {
        SpawnLung();
    }

    public void SpawnLung()
    {
        for (int x = 0; x < lungGrid.x; x++)
        {
            for (int y = 0; y < lungGrid.y; y++)
            {
                Instantiate(lungHole, new Vector3(x - lungGrid.x / 2, y - lungGrid.y / 2, 0) * spacing, Quaternion.identity, transform);
            }
        }
    }

}
