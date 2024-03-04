using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {

    public float powerUseage;
    //public int type;    

    public bool hasEffect;
    public bool hasInstaEffect;

    virtual public void Effect(Player player){}
    virtual public void InstaEffect(Player player){}

    public bool PowerUseage(Player player)
    {
        if (player.powerLeft >= powerUseage)
        {
            player.powerLeft -= powerUseage;
            return true;
        }
        return false;
    }

}