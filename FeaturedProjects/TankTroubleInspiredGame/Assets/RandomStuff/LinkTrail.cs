using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkTrail : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        StartCoroutine(IDisapear());
    }

    public IEnumerator IDisapear()
    {
        float fadeSpeed = 2.0F;

        while(spriteRenderer.color.a > 0)
        {
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - fadeSpeed * Time.deltaTime);

            yield return null;
        }

        Destroy(gameObject);
    }
}
