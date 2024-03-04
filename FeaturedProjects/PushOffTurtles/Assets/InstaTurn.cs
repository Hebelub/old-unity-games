using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaTurn : Power {

    private void Start()
    {
        //type = 1;
        powerUseage = 0.75f;

        hasEffect = true;
        hasInstaEffect = false;
    }

    public override void Effect(Player player)
    {
        if(player.downClick && PowerUseage(player))
        {
            player.transform.Rotate(0, 180, 0);

        }

    }

    public override void InstaEffect(Player player)
    {

    }

}
