using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void Teleport()
    {
        int s = LevelGenerator.instance.level + 3;
        int a = LevelGenerator.instance.level / 2;

        Vector3 v = new Vector3(Random.Range(0, s) + a, Random.Range(0, s) + a, PlayerMovement.instance.transform.position.z);

        PlayerMovement.instance.transform.position = v;

    }
}
