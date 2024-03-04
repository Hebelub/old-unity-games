using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Power
{

    float force = 1.6f;

    private void Start()
    {

        //type = 2;
        powerUseage = 0.006f;

        hasEffect = true;
        hasInstaEffect = false;

    }

    public override void Effect(Player player)
    {

        if (player.stayClick && PowerUseage(player))
        {
            player.moveForce += player.transform.forward * force * player.moveSpeed;
        }
    }

    public override void InstaEffect(Player player)
    {

    }


}