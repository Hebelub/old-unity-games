using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayer : MonoBehaviour
{
    public Player displayedPlayer;

    public TextMeshProUGUI tName;
    public TextMeshProUGUI tExtraTime;
    public TextMeshProUGUI tExtraBonusTime;

    public TextMeshProUGUI tTimeLeft;

    public void Display(Player player)
    {
        displayedPlayer = player;

        tName.text = player.name;
        tExtraTime.text = player.extraTime.ToString();
        tExtraBonusTime.text = player.extraBonusTime.ToString();

        tTimeLeft.text = player.timeLeft.ToString();

    }
}

public class Player
{
    public string name;
    public float extraTime;
    public float extraBonusTime;

    public float timeLeft;

    public void YourTurn()
    {

    }
    public void Iterate()
    {

    }
    public void NextPlayer()
    {

    }

}
