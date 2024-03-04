using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    static public NoteLine[] noteLines;

    private void Start()
    {
        noteLines = GetComponentsInChildren<NoteLine>();

        noteLines[0].cleff = M.cleffG;
        noteLines[1].cleff = M.cleffF;

    }

}
