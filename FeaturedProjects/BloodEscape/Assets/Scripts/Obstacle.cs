using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float damage; // Oxygen absorbtion level

    private SpriteRenderer sr;

	void Start ()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        RandomzeObstacle();

        UpdateStuff();
	}
	
    public void RandomzeObstacle()
    {
        do damage = Random.value;
        while (damage == 0);

        GameManager.instance.spawnedOxygenAbsorbtionRelativeToOxygen += damage;
    }

    public void UpdateStuff()
    {
        sr.color = new Color(1, 1 - damage, 1 - damage);

        if (damage <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //// If this should work you have to take it out of the parent first, but I don't want it anyways...
        Destroy(FindRoot());
    }
    private GameObject FindRoot()
    {
        // Chenges climbing transform to the parent of itselves whenever it do not have the ObstacleRoot component on it
        Transform climbingTransform = transform;
        while (climbingTransform.GetComponent<ObstacleRoot>() == null)
        {
            climbingTransform = climbingTransform.parent;
        }
        return climbingTransform.gameObject;
    }

}
