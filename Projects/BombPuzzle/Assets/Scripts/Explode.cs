using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : Entity {

    public float explotionForce = 100f;
    public float affectRadius = 10f;

    public override void OnClick()
    {
        Collider2D[] affectedColliders = Physics2D.OverlapCircleAll(transform.position, affectRadius);

        foreach (Collider2D nearbyObject in affectedColliders)
        {
            Entity entity = nearbyObject.GetComponent<Entity>();
            Rigidbody2D nearbyRb = entity.rb;

            if (nearbyRb != null)
            {
                // Finding the direction of the force
                Vector2 direction = (Vector2)nearbyObject.transform.position - (Vector2)transform.position;

                if (direction.magnitude > 0)
                {
                    nearbyRb.AddForce(direction.normalized * explotionForce / direction.magnitude);

                }

            }

        }
    }

    
}
