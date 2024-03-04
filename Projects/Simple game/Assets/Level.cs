using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    protected void ToggleChildren()
    {
        foreach (Transform child in transform)
        {
            bool isActive = child.gameObject.activeInHierarchy;
            child.gameObject.SetActive(!isActive);
        }
    }
}
