using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : Item
{
    public Vector3 wantedScale;

    public float growSpeed;

    private void Awake()
    {
        growSpeed = transform.localScale.magnitude / 4f;
        wantedScale = transform.localScale;
        //StartCoroutine(IGrow());
    }

    private void OnMouseDown()
    {
        wantedScale += wantedScale.normalized * growSpeed;

        transform.localScale = wantedScale;

        Debug.Log(wantedScale);
    }

    //public IEnumerator IGrow()
    //{
    //    while (true)
    //    {


    //        transform.localScale += wantedScale;

    //        yield return true;
    //    }
    //}

}
