using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed = 0.1f;

    public Inventory inventory;

    public Target target;
    Queue<Target> targets = new Queue<Target>();

    public Target targetGo;

    public Island nextIsland;
    public bool hasIsland = false;

    public Rigidbody2D rb;

    private void OnMouseDown()
    {
        inventory.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;

            MoveToIsland(worldPosition);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Island>() == nextIsland)
        {
            if (hasIsland)
            {
               // bool closeTargetIsland = Vector3.Distance(transform.position, nextIsland.transform.position) < 7.5f;
              //  Debug.Log(Vector3.Distance(transform.position, nextIsland.transform.position));
                //if (closeTargetIsland)
                //{
                    nextIsland.Explore();
                    hasIsland = false;
                //}
            }
        }
    }

    public void Start()
    {
        target = Instantiate(targetGo, transform.position, Quaternion.identity); 
        StartCoroutine(IMove());
    }

    IEnumerator IMove()
    {
        while(true)
        {
            bool isClose = Vector3.Distance(transform.position, target.transform.position) < 2.0f;
            bool isSlow = rb.velocity.magnitude < 3.0f;
            if ((isClose || isSlow) && targets.Count > 0)
            {
                target.alive = false;
                target = targets.Dequeue();
                // Flip boat
                if (target.transform.position.x < transform.position.x)
                {
                    transform.localScale = Vector3.one + Vector3.left * 2;
                }
                else
                {
                    transform.localScale = Vector3.one;
                }
            }
            else if (isClose)
            {
                // target.alive = false;
                target.gameObject.SetActive(false);
            }

            var direction = target.transform.position - transform.position;
            rb.velocity = direction.normalized * speed;

            if (isClose)
            {
                rb.velocity = Vector3.zero;
            }
            if (isSlow)
            {
                //if (hasIsland)
                //{
                //    bool closeTargetIsland = Vector3.Distance(transform.position, nextIsland.transform.position) < 7.5f;
                //    Debug.Log(Vector3.Distance(transform.position, nextIsland.transform.position));
                //    if (closeTargetIsland)
                //    {
                //        nextIsland.Explore();
                //        hasIsland = false;
                //    }
                //}
            }
            // transform.position = Vector3.Lerp(transform.position, target, speed);
            yield return null;
        }
    }

    public void MoveToIsland(Vector3 moveTo)
    {
        var go = Instantiate(targetGo, moveTo, Quaternion.identity);
        targets.Enqueue(go);
    }

    public void SetTargetIsland(Island tIsland)
    {
        hasIsland = true;
        nextIsland = tIsland;
    }

}
