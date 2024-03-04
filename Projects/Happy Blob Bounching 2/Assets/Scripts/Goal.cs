using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public Vector3 wantedPosition = Vector3.one;
    public Vector3 wantedScale = Vector3.one;

    public float moveSpeed = 1f;
    public float scaleSpeed = 1f;

    public float betweenA;
    public float betweenB;
    public float minScale;
    public float maxScale;

    private void Start()
    {
        wantedPosition = transform.position;
        wantedScale = transform.localScale;

        StartCoroutine(ITransition());
    }

    public void NewPosition()
    {
        wantedPosition.x = Random.Range(betweenA, betweenB);
        wantedScale.x = Random.Range(minScale, maxScale);
    }

    public IEnumerator ITransition()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, wantedPosition, moveSpeed * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, wantedScale, scaleSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
