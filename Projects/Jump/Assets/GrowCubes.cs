using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCubes : Cube
{
    private void Start()
    {
        base.Start();
    }

    private void OnMouseDown()
    {
        StartCoroutine(IGrow());
    }

    public IEnumerator IGrow()
    {
        Collider[] overlaps = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

        foreach (Collider col in overlaps)
        {
            if (col.gameObject != gameObject)
            {
                col.gameObject.GetComponent<Cube>().transform.localScale += Vector3.one;
            }

            yield return new WaitForSeconds(0.05f);
        }

        Hit(upgrades.bombDamage);
    }
}
