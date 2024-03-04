using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCoin : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
        StartCoroutine(IFade());
    }

    public IEnumerator IFade()
    {
        while (transform.localScale.x > 0.02f)
        {
            yield return null;
            transform.localScale = transform.localScale -= Vector3.one * 0.01f;
        }
        Destroy(gameObject);
    }
}
