using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public void GroundSize()
    {
        Upgrade.groundSize += Upgrade.groundGrowth;
        StartCoroutine(GroundSizeAnimation());
    }
    public void CheeseValue()
    {

    }
    public void DropProbability()
    {

    }

    private IEnumerator GroundSizeAnimation()
    {
        while (GameManager.Instance.groundTransform.localScale.magnitude < Upgrade.groundSize.magnitude)
        {
            GameManager.Instance.groundTransform.localScale += Upgrade.groundGrowth * Upgrade.groundGrowthSpeed * Time.deltaTime;
            if (transform.localScale.magnitude > Upgrade.groundSize.magnitude)
            {
                transform.localScale = Upgrade.groundSize;
            }
            yield return null;
        }
    }

}
