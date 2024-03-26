using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : Obs {

    public float moveSpeed;
    private Transform target;

    private Obstacle obstacle;

	void Start ()
    {
        obstacle = GetComponent<Obstacle>();

        target = GameManager.instance.player.transform;

        RandomValues();

        StartCoroutine(Motion());
	}

    public void RandomValues()
    {
        // Geting random moveSpeed
        moveSpeed = Random.value * 3 + 0.1f;
    }

    private IEnumerator Motion()
    {
        while (true)
        {
            Vector3 wantedPosition;
            if (obstacle.damage > 0)
            {
                wantedPosition = target.position;
            }
            else
            {
                wantedPosition = (transform.position - target.position) * 2 + target.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, wantedPosition, moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
