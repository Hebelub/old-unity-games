using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Alive
{
    public float nextEgg;
    public float slayChance = 0.1f;

    public GameObject egg;
    public GameObject meat;
    public GameObject feather;

    void Start()
    {
        StartCoroutine(IWalk());

        //StartCoroutine(IEggClock());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Sapling>() != null)
        {
            Destroy(collision.gameObject);
            LayEgg();
        }
    }

    private void OnMouseDown()
    {
        if (Random.value < slayChance)
        {
            Slay();
        }
    }

    public void Slay()
    {
        DropItem(meat, 0f);

        Destroy(gameObject);
    }

    //public IEnumerator IEggClock()
    //{
    //    while (true)
    //    {
    //        nextEgg = Random.Range(8f, 24f);

    //        yield return new WaitForSeconds(nextEgg);

    //        LayEgg();
    //    }
    //}

    public void LayEgg()
    {
        DropItem(egg, 1f);

    }

}
