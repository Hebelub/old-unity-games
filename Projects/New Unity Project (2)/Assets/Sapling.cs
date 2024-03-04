using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : Obj
{
    public GameObject tree;

    public float wait;

    private void Start()
    {
        wait = Random.Range(10f, 30f);

        StartCoroutine(IGrow());
    }

    public IEnumerator IGrow()
    {
        yield return new WaitForSeconds(wait);

        Grow();
    }

    public void Grow()
    {
        DropItem(tree, 0f);

        Destroy(gameObject);
    }
}
