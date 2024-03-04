using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPower : Power
{

    private void Start()
    {
        //type = 1;
        powerUseage = 0.25f;

        hasEffect = true;
        hasInstaEffect = false;
    }

    public override void Effect(Player player)
    {

        if (player.downClick && PowerUseage(player))
        {

                player.moveForce += Vector3.up * player.jumpHeight;

        }
    }

    public override void InstaEffect(Player player)
    {
        
    }


}