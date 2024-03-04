using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        BlockInfo info = other.GetComponent<BlockInfo>();

        if (info.goalDestroy)
            GameManager.Instance.destroyGoalsLeft--;
        if (info.looseOnDesroy)
            Loose();
        Destroy(other.transform.gameObject);
        if (GameManager.Instance.destroyGoalsLeft == 0)
        {
            Win();
        }
    }

    public void Loose()
    {
        Debug.Log("You lost");
    }

    public void Win()
    {
        Debug.Log("You won");
    }

}
