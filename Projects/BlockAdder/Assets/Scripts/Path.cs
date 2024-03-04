using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : Obs {

    // Need to store an array of positions
    public Vector3 startPosition;
    public Vector3 endPosition;

    private bool startPositionWanted;

    // Wanted position
    public Vector3 wantedPosition;

    // The object that is moving along the path
    public Transform movingObject;

    // End object
    public Transform endTransform;

    // The speed of the movement
    public float movementSpeed;

	void Start () {
        CreateRandomPath();
	}

    void Update () {
		
	}

    public void CreateRandomPath()
    {
        // Assigning the startposition
        startPosition = transform.position;

        // Geting random end position
        do
        {
            endPosition = GameManager.instance.GetRadomPointOnGrid();
        }
        while ((startPosition - endPosition).magnitude < 2);

        // Finding a random speed to the obstacle
        movementSpeed = Random.value / 8 + 0.01f;

        // Seting the end joint to the correct position
        endTransform.position = endPosition;

        wantedPosition = endPosition;

        // Starting the coroutine
        StartCoroutine(Motion());

    }

    public IEnumerator Motion()
    {
        while(true)
        {
            while(movingObject.position != wantedPosition)
            {
                // Move along path...
                movingObject.position = Vector3.MoveTowards(movingObject.position, wantedPosition, movementSpeed);

                yield return true;
            }
            startPositionWanted = !startPositionWanted;
            wantedPosition = (startPositionWanted) ? endPosition : startPosition;
        }
    }

}
