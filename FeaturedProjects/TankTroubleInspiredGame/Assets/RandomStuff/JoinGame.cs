using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour
{
    public const int maxControllers = 8;

    public Tank[] tanks = new Tank[maxControllers];

    void Update()
    {
        for (int i = 0; i < maxControllers; i++)
        {
            string player = "C" + (i + 1).ToString() + " A";
            
            if (Input.GetButtonDown(player))
            {
                CreateTank(i);
            }
        }
    }

    public GameObject tank;

    public void CreateTank(int playerNumber)
    {
        if(tanks[playerNumber] == null)
        {
            Tank newTank = Instantiate(tank).GetComponent<Tank>();
            newTank.PlayerNumber = playerNumber + 1;
            tanks[playerNumber] = newTank;
        }

        tanks[playerNumber].RandomizeColors();
    }
}
