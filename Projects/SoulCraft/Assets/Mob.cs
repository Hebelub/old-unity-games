using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float health = 10f;
    public float movement_speed = 3f;
    public float damage = 1f;

    public List<Item> loot;

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0) Kill();
    }

    public void Kill()
    {
        DropLoot();
        Destroy(gameObject);
    }

    public void DropLoot()
    {

    }
}

public class Item
{

}
