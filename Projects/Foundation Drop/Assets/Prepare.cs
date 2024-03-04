using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prepare : MonoBehaviour {

    public Transform objects;
    private Transform moving;

    private int DestroyGoalQuantity = 0;

	void Start () {
        moving = objects.GetChild(1);
        PrepareGame();
	}

    private void PrepareGame()
    {
        DontDestroyOnLoad(transform.root);
        PrepareLevel();
    }

    private void PrepareLevel()
    {
        // Need to find objects

        Count_WillBeDestroyed();
    }
	
    private void Count_WillBeDestroyed()
    {
        int childQuantity = moving.childCount;

        for (int i = 0; i < childQuantity; i++)
        {
            Transform child = moving.GetChild(i);

            BlockInfo info = child.GetComponent<BlockInfo>();

            if (info.goalDestroy)
            {
                DestroyGoalQuantity++;
            }

        }

        GameManager.Instance.destroyGoalsLeft = DestroyGoalQuantity;

    }

}
