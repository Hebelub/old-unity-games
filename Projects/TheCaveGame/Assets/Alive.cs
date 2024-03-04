using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public float health = 1.0f;

    public void Move(Vector3 direction)
    {
        if (CanWalk(direction))
        {
            M(direction);
        }
        else if (CanWalk(Vector3.up) && CanWalk(direction + Vector3.up))
        {
            M(direction + Vector3.up);
        }

        void M(Vector3 d)
        {
            transform.position += d;
        }

        bool CanWalk(Vector3 dir)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + dir, 0.1f);

            bool canWalk = true;

            foreach (Collider o in colliders)
            {
                Block block = o.GetComponentInParent<Block>();
                if (block != null)
                {
                    // canWalk = false;

                } 
            }

            return canWalk;
        }
    }
}
