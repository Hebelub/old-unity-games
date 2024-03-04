using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Cube
{
    private void Start()
    {
        base.Start();
    }

    private void OnMouseDown()
    {
        StartCoroutine(IExplode());
    }

    public IEnumerator IExplode()
    {
        Collider[] overlaps = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

        foreach (Collider col in overlaps)
        {
            if(col.gameObject != gameObject)
            {
                col.gameObject.GetComponent<Cube>().Hit(upgrades.bombDamage);
            }

            yield return new WaitForSeconds(0.05f);
        }

        Hit(upgrades.bombDamage);
    }
}
