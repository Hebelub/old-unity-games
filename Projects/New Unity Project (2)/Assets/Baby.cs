using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Alive
{
    public GameObject human;

    public float growTime = 20f;

    private void Start()
    {
        StartCoroutine(IGrow());

        IEnumerator IGrow()
        {
            yield return new WaitForSeconds(growTime * Random.value);

            DropItem(human, 0f);
            Destroy(gameObject);
        }
    }
}
