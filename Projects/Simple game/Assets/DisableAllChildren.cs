using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllChildren : MonoBehaviour
{
    private void Awake()
    {
        DisableRest(transform);
    }

    public void DisableRest(Transform currentChild)
    {
        foreach (Transform child in currentChild)
        {
            child.gameObject.SetActive(false);
            DisableRest(child);
        }
    }
}
