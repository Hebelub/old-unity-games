using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public class Board
    {
        public Turn turn = Turn.WHITE;

        enum Turn
        {
            BLACK,
            WHITE
        }
    }

    public Board FindBestMove(Board board)
    {

    }
}
