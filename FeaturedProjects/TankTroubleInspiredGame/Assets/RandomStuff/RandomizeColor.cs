using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
