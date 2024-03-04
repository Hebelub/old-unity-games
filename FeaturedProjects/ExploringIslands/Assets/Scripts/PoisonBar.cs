using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBar : MonoBehaviour
{
    public GameObject sprite;
    public float spacing = 1.5f;

    public float max = 10;

    public float SpawnChildren(float quantity)
    {
        KillAllChildren();

        if (quantity > max)
        {
            quantity = max;

        }

        for (int i = 0; i < quantity; i++)
        {
            GameObject go = Instantiate(sprite, Vector3.right * i * spacing + transform.position, Quaternion.identity, transform);
        }

        return quantity;
    }

    public void KillAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
