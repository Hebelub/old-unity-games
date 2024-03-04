using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Block
{
    public void Dig()
    {

        StartCoroutine(IDig());

        IEnumerator IDig()
        {
            while(true)
            {
                transform.localScale -= Vector3.one * 0.0001f;
                if (transform.localScale.x <= 0.001)
                {
                    Destroy(gameObject);
                }

                yield return null;
            }
        }
    }

}
