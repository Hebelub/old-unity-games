using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Color activeColor;
    public Color notActiveColor;

    public MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponentInChildren<MeshRenderer>();
        mr.material.color = notActiveColor;
    }

    public void ActivateColor(bool b)
    {
        if (b)
        {
            mr.material.color = activeColor;
        }
        else
        {
            mr.material.color = notActiveColor;
        }
    }
}
