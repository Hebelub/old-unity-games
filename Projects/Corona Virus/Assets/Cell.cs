using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool infected = false;

    private void FixedUpdate()
    {
        if (infected)
        {
            if (Random.value < 0.025f)
                Instantiate(GameManager.instance.virus, Random.insideUnitSphere.normalized * transform.localScale.x / 2f + transform.position, Quaternion.identity);
        }
    }

}
