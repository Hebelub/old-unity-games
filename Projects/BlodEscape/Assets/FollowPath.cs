using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    public Transform wantedDestination;

    public Transform start;
    public Transform end;

    public float minSpeed;
    public float maxSpeed;

    public float movementSpeed;

	void Start ()
    {
        GetDesttinations();

        RandomSpeed();

        StartCoroutine(IMoveInPath());
	}
	
    public void RandomSpeed()
    {
        movementSpeed = Random.Range(minSpeed, maxSpeed);
    }

    public void GetDesttinations()
    {
        start = transform.parent;

        end = start.GetComponentInChildren<Destination>().transform;

        if (wantedDestination == null)
        {
            wantedDestination = start;
        }
    }

    public IEnumerator IMoveInPath()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, wantedDestination.position, movementSpeed * Time.deltaTime);

            if (transform.position == wantedDestination.position)
            {
                wantedDestination = (wantedDestination == start) ? end : start;
            }

            yield return null;
        }
    }

}
