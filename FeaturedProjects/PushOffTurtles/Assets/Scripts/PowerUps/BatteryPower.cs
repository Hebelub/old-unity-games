using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPower : Power
{
    private float value = 1f;

    private void Start()
    {
        //type = 1;
        powerUseage = 0f;

        hasEffect = false;
        hasInstaEffect = true;
    }

    public override void Effect(Player player)
    {
       
    }

    public override void InstaEffect(Player player)
    {
        player.wealth += value;
    }

}