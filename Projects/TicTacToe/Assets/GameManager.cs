using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int turn;
    public int playerTurn;

    public Player[] players = new Player[2];

    private void Start()
    {
        players[0] = new Player("Player 1", true);
        players[1] = new Player("Player 2", false);
    }

    public void NextTurn()
    {
        turn++;
        playerTurn = turn % 2;



    }

}
