using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : Power {

    float ammount = 1.2f;

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
        player.transform.localScale *= ammount;
        player.rb.mass *= ammount;
    }

}
