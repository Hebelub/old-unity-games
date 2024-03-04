using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;

    public float currentTime = 0;

    public float countSpeed = 1; //    (+n): count up;    (-n): count down
    public bool isCounting = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTime += 60.0F * countSpeed;

            if(!isCounting)
                DisplayTime();
        }

        if(Input.GetKeyDown(KeyCode.Minus))
        {
            countSpeed *= -1;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isCounting = !isCounting;
        }

        if(isCounting)
        {
            currentTime += Time.deltaTime * countSpeed;
            DisplayTime();
        }
    }

    public bool IsZero()
    {
        if(currentTime <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DisplayTime()
    {
        float time = currentTime;

        if(time < 0)
        {
            time = 0;
            currentTime = 0;
            isCounting = false;
        }

        int hundreds = Mathf.FloorToInt((time - Mathf.Floor(time)) * 100);
        if (hundreds == 11) // To prevent glitching! :P
            hundreds = 12;
        int seconds = (int) time % 60;
        int minuttes = (int) (time % 3600) / 60;
        int hours = (int) (time % 216000) / 3600;

        string c = hundreds < 10 ? "0" + hundreds.ToString() : hundreds.ToString();
        string s = (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString()) + ".";
        string m = (minuttes < 10 ? "0" + minuttes.ToString() : minuttes.ToString()) + ":";
        string h = hours < 1 ? "" : hours.ToString() + ":";

        string toDisplay = h + m + s + c;

        timeDisplay.text = toDisplay;
    }
}
