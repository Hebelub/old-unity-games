using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoyBarrel : Barrel
{
    public float backTrackForce = 100.0F;

    public override void AnimateShot()
    {
        BackTrack(backTrackForce);
        NormalAnimation();
    }

    public void BackTrack(float force)
    {
        tank.rigidbody.AddForce(-tank.aimDirection.normalized * force);
    }

}
