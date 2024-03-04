using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    //public bool resizingGround = false;

    private Transform[] groundChilds = new Transform[4];

    public GameObject WallPrefab;

    private Transform[] walls = new Transform[4];

    public float wallThickness = 1;
    public float wallHeight = 1;

    void Start () {
        Vector3 groundStartingSize = transform.localScale;
        for (int i = 0; i < 4; i++)
            groundChilds[i] = transform.GetChild(i);
        for (int i = 0; i < 4; i++)
            walls[i] = Instantiate(WallPrefab).transform;
        ResizeGround(groundStartingSize);
    }
	
	void Update () {
        
	}

    public IEnumerator ResizeGroundSmooth (Vector3 newSize)
    {
        //if (resizingGround)
        //{
        //    // Should stop currently running coroutine
        //}
        //resizingGround = true;

        Vector3 currentSize = transform.localScale;
        Vector3 difference = newSize - transform.localScale;
        Vector3 changePerFrame = difference / 80;

        while (transform.localScale.magnitude < newSize.magnitude)
        {
            currentSize += changePerFrame;
            ResizeGround(currentSize);
            yield return null;
        }

        //resizingGround = false;
    }

    private void ResizeGround(Vector3 newSize)
    {
        transform.localScale = newSize;
        // Set wall 1
        walls[0].position = groundChilds[0].position + new Vector3(wallThickness / 2, (wallHeight + transform.localScale.y) / 2, 0);
        walls[0].localScale = new Vector3(wallThickness, wallHeight + transform.localScale.y, transform.localScale.z);
        // Set wall 2
        walls[1].position = groundChilds[1].position + new Vector3(-wallThickness / 2, (wallHeight + transform.localScale.y) / 2, 0);
        walls[1].localScale = new Vector3(wallThickness, wallHeight + transform.localScale.y, transform.localScale.z);
        // Set wall 3
        walls[2].position = groundChilds[2].position + new Vector3(0, (wallHeight + transform.localScale.y) / 2, wallThickness / 2);
        walls[2].localScale = new Vector3(transform.localScale.x + 2 * wallThickness, wallHeight + transform.localScale.y, wallThickness);
        // Set wall 4
        walls[3].position = groundChilds[3].position + new Vector3(0, (wallHeight + transform.localScale.y) / 2, -wallThickness / 2);
        walls[3].localScale = new Vector3(transform.localScale.x + 2 * wallThickness, wallHeight + transform.localScale.y, wallThickness);



    }
}
