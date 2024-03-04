using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    { 

            GameManager.instance.player.color = sr.color;
            GameManager.instance.cam.backgroundColor = sr.color;
        
    }
}
