using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treads : MonoBehaviour
{
    public GameObject leftTread;
    public GameObject rightTread;
    public Links leftLinks;
    public Links rightLinks;

    public void SetTreadColor(Color newColor)
    {
        leftTread.GetComponent<SpriteRenderer>().color = newColor;
        rightTread.GetComponent<SpriteRenderer>().color = newColor;
    }

    public void SetLinksColors(Color newColor)
    {
        leftLinks.SetColorOfLinks(newColor);
        rightLinks.SetColorOfLinks(newColor);
    }

}
