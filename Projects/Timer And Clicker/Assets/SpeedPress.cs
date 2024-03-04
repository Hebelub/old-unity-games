using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedPress : MonoBehaviour
{
    public TextMeshProUGUI clicksText;
    int clicks = 0;

    public Timer timer;

    bool isRevealed = false;

    bool hasStartedClicking = false;

    private void Awake()
    {
        Reveal(false);
    }

    private void Update()
    {
        if(isRevealed)
        {
            if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && !timer.IsZero())
            {
                if(!hasStartedClicking)
                {
                    timer.isCounting = true;
                    hasStartedClicking = true;
                }
                clicks += 1;
                UpdateText();
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                clicks = 0;
                UpdateText();
                Reveal(false);
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Reveal(true);
            }
        }
    }

    public void Reveal(bool reveal)
    {
        if(reveal)
        {
            timer.isCounting = false;
            timer.currentTime = 60.0F;
            timer.countSpeed = -1.0F;
            timer.DisplayTime();
        }
        else
        {
            hasStartedClicking = false;
            timer.currentTime = 0.0F;
            timer.countSpeed = 1.0F;
            timer.DisplayTime();
        }


        isRevealed = reveal;
        transform.GetChild(0).gameObject.SetActive(reveal);
    }

    public void UpdateText()
    {
        clicksText.text = clicks.ToString();
    }
}
