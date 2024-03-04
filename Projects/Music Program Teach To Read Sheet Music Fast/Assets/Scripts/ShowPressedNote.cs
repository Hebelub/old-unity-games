using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is to show the last pressed placement of what you played on the piano
public class ShowPressedNote : MonoBehaviour
{
    public Color hitColor = Color.grey;
    public Color missColor = Color.red;

    public bool didHit;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (didHit) 
        {
            spriteRenderer.color = hitColor;
        }
        else
        {
            spriteRenderer.color = missColor;
        }
    }
}
