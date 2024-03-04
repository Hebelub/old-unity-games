using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterPlayer : Power
{
    float ammount = 1.4f;

    private void Start()
    {
        powerUseage = 0f;

        hasEffect = false;
        hasInstaEffect = true;
    }

    public override void Effect(Player player)
    {

    }

    public override void InstaEffect(Player player)
    {
        player.moveSpeed *= ammount;
        player.turnSpeed *= ammount;
    }

}
