using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnTouch : MonoBehaviour
{
    public float damage = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        Health hitHealth = collision.gameObject.GetComponent<Health>();
        if(hitHealth != null)
        {
            hitHealth.Hit(damage);
        }
    }
}
