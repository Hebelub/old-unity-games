using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Obj
{
    public GameObject chicken;

    public float wait;

    private void Start()
    {
        wait = Random.Range(10f, 30f);

        StartCoroutine(IHatch());
    }

    public IEnumerator IHatch()
    {
        yield return new WaitForSeconds(wait);

        Hatch();
    }

    public void Hatch()
    {
        DropItem(chicken, 0f);

        Destroy(gameObject);
    }
}
