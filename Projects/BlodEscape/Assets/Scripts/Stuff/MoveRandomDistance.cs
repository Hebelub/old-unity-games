using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomDistance : MonoBehaviour {

    public float distance;

    public float minDistance;
    public float maxDistance;

    public bool rounDistanceToNearestInt;

	void Start () {
        RandomDistance();
	}
	
    public void RandomDistance()
    {
        distance = Random.Range(minDistance, maxDistance);

        Debug.Log(distance);

        if (rounDistanceToNearestInt)
        {
            distance = Mathf.RoundToInt(distance);
        }

        Debug.Log(distance);

        transform.position += (Vector3)Random.insideUnitCircle.normalized * distance;
    }

}
