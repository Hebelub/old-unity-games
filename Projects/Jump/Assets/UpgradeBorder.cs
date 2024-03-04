using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBorder : MonoBehaviour
{
    Vector3 direction = Vector3.zero;

    private void OnMouseDown()
    {
        Upgrade();
    }

    public void Upgrade()
    {
        Destroy(gameObject);
    }
}
