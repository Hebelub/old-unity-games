using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeft : MonoBehaviour
{
    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.instance.player.timeLeft += 60;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.instance.player.timeLeft -= 60;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameManager.instance.player.timeLeft += 10;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameManager.instance.player.timeLeft -= 10;
        }

        if (GameManager.instance.player.timeLeft < 0)
        {
            GameManager.instance.player.timeLeft = 0;
        }
        GameManager.instance.timeLeftText.text = Mathf.RoundToInt(GameManager.instance.player.timeLeft).ToString();

    }
}
