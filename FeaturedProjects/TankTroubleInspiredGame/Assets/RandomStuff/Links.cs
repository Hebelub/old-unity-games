using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Links : MonoBehaviour
{

    public Transform trail;
    public GameObject linkTrail;

    private void Start()
    {
        StartCoroutine(IAnimateLinks());
    }

    public IEnumerator IAnimateLinks()
    {
        float hasMoved = 0.0F;
        float oneLinkDistance = 0.1F;

        Vector3 previousPosition = transform.position;

        Queue<GameObject> linkTrails = new Queue<GameObject>();

        while(true)
        {
            float moveDistance = DistanceMoved();
            hasMoved += moveDistance; 
            transform.position += transform.right * moveDistance;

            if (hasMoved > oneLinkDistance)
            {
                int timesOneLinkDistance = (int)(hasMoved / oneLinkDistance);

                transform.position -= transform.right * oneLinkDistance * timesOneLinkDistance;
                hasMoved -= oneLinkDistance * timesOneLinkDistance;

                // This for loop is out of order ...
                for (int i = timesOneLinkDistance - 1; i >= timesOneLinkDistance - 1; i--)
                {
                    GameObject newTrail = Instantiate(linkTrail, trail.position /*- transform.right * oneLinkDistance * i*/, transform.rotation);
                    linkTrails.Enqueue(newTrail);
                    while(linkTrails.Count >= 16)
                    {
                        GameObject oldestTrail = linkTrails.Dequeue();
                        Destroy(oldestTrail);
                    }
                }
            }

         //   while (hasMoved > oneLinkDistance)
         //   {
         //       transform.position -= transform.right * oneLinkDistance;
         //       hasMoved -= oneLinkDistance;
         //
         //       Instantiate(linkTrail, trail.position, transform.rotation);
         //   }

            previousPosition = transform.position;

            yield return null;
        }

        float DistanceMoved()
        {
            return (previousPosition - transform.position).magnitude;
        }

    }

    public void SetColorOfLinks(Color newColor)
    {
        SpriteRenderer[] spriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = newColor;
        }

    }
}
