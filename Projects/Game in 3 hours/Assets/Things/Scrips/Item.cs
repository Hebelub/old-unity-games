using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        VisualDifference(0.5f);
    }

    private void OnMouseExit()
    {
        VisualDifference(1f);
    }

    public void VisualDifference(float transperantValue)
    {
        Color c = mr.material.color;
        mr.material.color = new Color (c.r, c.g, c.b, transperantValue);

    }

}
