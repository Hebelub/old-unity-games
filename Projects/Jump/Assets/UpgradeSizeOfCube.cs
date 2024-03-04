using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSizeOfCube : MonoBehaviour
{
    public Transform cubeMaxSize;

    public Transform[] upgradeDirections;

    public void Upgrade(UpgradeDirection uD)
    {
        GameManager.instance.Money -= uD.costOfDirection;

        uD.costOfDirection *= 5;

        uD.transform.position += uD.transform.right * uD.direction.x / 2 +
                                 uD.transform.up * uD.direction.y / 2 +
                                 uD.transform.forward * uD.direction.z / 2;

        foreach (Transform uT in upgradeDirections)
        {
            uT.localScale += uD.direction;
        }
        uD.transform.localScale -= uD.direction;
        //uD.transform.localScale += Vector3.one - uD.direction;
        cubeMaxSize.localScale += uD.direction;

        GameManager.instance.upgrades.maxCubeSize = cubeMaxSize.localScale;
  //      GameManager.instance.worldSize = Vector3.one * (Mathf.Max(Mathf.Max(transform.localScale.x, transform.localScale.y), transform.localScale.z) / 2);
       // Camera.main.transform.position = Vector3.forward * -GameManager.instance.worldSize.magnitude * 2;

    }
}
