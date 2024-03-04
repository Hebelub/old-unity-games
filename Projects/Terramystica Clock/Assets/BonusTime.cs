using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTime : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.instance.player.extraTime += 10;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.instance.player.extraTime -= 10;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.instance.player.extraTime += 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.instance.player.extraTime -= 1;
        }

        GameManager.instance.turnNumberText.text = "Turn: " + (GameManager.instance.player.playedTurns - 1).ToString() + 
                                                   "        Bonus: " + GameManager.instance.player.extraTime.ToString();

    }
}
