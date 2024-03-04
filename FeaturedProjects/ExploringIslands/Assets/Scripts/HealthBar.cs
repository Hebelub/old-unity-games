using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float full = 40;

    public void NewHealth(float newHealth)
    {
        if (newHealth > full)
        {
            newHealth = full;
            GameManager.instance.health = full;
        }
        transform.GetChild(0).localScale = new Vector3 (newHealth / full, 1, 1);

    }

}
