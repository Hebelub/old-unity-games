using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Obs {

    public float rotationSpeed;
    public float distance;

    GameManager gm;

	void Start ()
    {
        gm = GameManager.instance;

        RandomValues();

        StartCoroutine(Motion());
	}

    public void RandomValues()
    {
        // Gets the abs of the rotation speed
        rotationSpeed = Random.value * 25 + 10f;
        // Finds direction of the rotation
        if (Random.Range(0, 2) == 0)
        {
            // It will rotation counterclockwise
            rotationSpeed *= -1;
        }

        float maxDistance = (gridCoords.x < gridCoords.y) ? gridCoords.x : gridCoords.y; ///////////// -------------> /////////////// This is a little bit wierd ._.
        maxDistance = (gm.gridSize.x - gridCoords.x < maxDistance) ? gm.gridSize.x - gridCoords.x : (gm.gridSize.y - gridCoords.y < maxDistance) ? gm.gridSize.y - gridCoords.y : maxDistance;

        Vector3 rotatingEnd;

        // Will get the distance of the rotation obstacle
        distance = Random.Range(2, maxDistance);

        // Sets the distance
        rotatingEnd = transform.GetChild(1).position = distance * Vector3.right;

        // Seting the starting rotation
        transform.eulerAngles = ((360f / 4) * Random.Range(0, 4) * Vector3.forward);

        if(!GameManager.instance.VectorInAllowedPosition(rotatingEnd, 8))
        {
            Debug.Log("It happened now, the rotation of the transform is: " + transform.rotation);
            transform.eulerAngles += ((360f / 2) * Vector3.forward);
        }

    }

    private IEnumerator Motion()
    {
        while(true)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
