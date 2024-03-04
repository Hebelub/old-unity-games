using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteRocketProjectile : Projectile
{
    public bool noSteering = false;

    private void Update()
    {
        if(!noSteering)
        {
            rb.velocity = tank.aimDirection.normalized * shootVelocity;
            float angle = tank.DirectionToAngle(tank.aimDirection);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            float angle = tank.DirectionToAngle(rb.velocity);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public override void Start()
    {
        base.Start();
        tank.events.OnShoot += NoSteering;
    }

    public void NoSteering()
    {
        noSteering = true;
    }

    public override void Kill()
    {
        tank.events.OnShoot -= Kill;
        Destroy(gameObject);
    }

}
