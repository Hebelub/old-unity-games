using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDirection : MonoBehaviour
{
    public UpgradeSizeOfCube uSOC;

    public float costOfDirection = 1f;

    public bool doesAfford = false;

    public Vector3 direction;

    private void Start()
    {
        uSOC = GetComponentInParent<UpgradeSizeOfCube>();
    }

    private void OnMouseEnter()
    {
        
    }
    private void OnMouseExit()
    {
        
    }
    private void OnMouseDown()
    {
        if (costOfDirection <= GameManager.instance.Money)
            uSOC.Upgrade(this);
    }
}
