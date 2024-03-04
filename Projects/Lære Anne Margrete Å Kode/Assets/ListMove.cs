using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListMove : MonoBehaviour
{

    public float moveSpeed = 0.01f;

    // public bool loop = true;

    public List<Transform> stopPositions = new List<Transform>();

    private void Start()
    {
        foreach (Transform t in transform.GetChild(0))
        {
            stopPositions.Add(t);
        }

        StartCoroutine(MoveToPosition());
    }

    Transform temp = null;

    public IEnumerator MoveToPosition()
    {

        while(true)
        {

            foreach (Transform wantedPosition in stopPositions)
            {
                if (temp == null) temp = wantedPosition;

                float t = 0f;

                // Vector3 startPosition = transform.position;

                // float distance = startPosition.magnitude - wantedPosition.magnitude;

                //bool instaEnd = false;

                //if (distance == 0)
                //{
                //    instaEnd = true;
                //}

                while (t < 1 /*&& !instaEnd*/)
                {

                    t += moveSpeed * Time.deltaTime;

                    transform.position = Vector3.Lerp(temp.position, wantedPosition.position, t);

                    yield return null;
                }

                transform.position = wantedPosition.position;

                temp = wantedPosition;
            }

            yield return null;
        }
    }

}
