using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBattery : Power
{
    float ammount = 0.5f;

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
        player.maxPower += ammount;
    }

}
