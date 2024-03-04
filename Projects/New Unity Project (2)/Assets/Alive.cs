using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : Obj
{
    public Vector3 wantedPosition;

    public float walkingSpeed = 1f;
    public float walkingDistance = 3f;
    public float walkingWait = 5f;

    public float timeToNextWalk = 0f;

    public IEnumerator IWalk()
    {
        Vector3 startPosition;

        float t = 0f;

        while(true)
        {
            wantedPosition = transform.position + (Vector3)Random.insideUnitCircle * walkingDistance;

            startPosition = transform.position;

            while(t < 1)
            {
                if (GetComponent<Entity>().attachedToMouse)
                {
                    t = 0;

                    while (GetComponent<Entity>().attachedToMouse)
                        yield return null;

                    Vector3 screenPoint = Input.mousePosition;
                    screenPoint.z = 10.0f; //distance of the plane from the camera
                    startPosition = Camera.main.ScreenToWorldPoint(screenPoint);
                    wantedPosition = Camera.main.ScreenToWorldPoint(screenPoint);

                }

                transform.position = Vector3.Slerp(startPosition, wantedPosition, t);

                t += 1 / walkingSpeed * Time.deltaTime;

                yield return null;
            }
            t = 0;

            timeToNextWalk = walkingWait * Random.value;

            yield return new WaitForSeconds(timeToNextWalk);

        }

    }

    public bool startAutomatically = false;

    void Start()
    {
        if (startAutomatically) StartCoroutine(IWalk());
    }

    void Update()
    {
        
    }
}
