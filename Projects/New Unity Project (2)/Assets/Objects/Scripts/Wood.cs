using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Obj
{

    public float chopChance;

    public AudioSource chopSound;

    public GameObject log;
    public GameObject sapling;
    public GameObject fruit;

    private void OnMouseDown()
    {
        if (Random.value < chopChance)
        {
            ChopTree();
        }
    }

    public void ChopTree()
    {
        DropItem(log, 0f);

        int saplingDrops = Random.Range(0, 5);
        for (int i = 0; i < saplingDrops; i++)
        {
            DropItem(sapling, 3f);
        }

        // Create sound <---

        Destroy(gameObject);

    }

}
