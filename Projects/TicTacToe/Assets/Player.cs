using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int score;

    public string name;

    public bool isX;

    public Player(string name, bool isX)
    {
        score = 0;
        this.name = name;
        this.isX = isX;
    }

}
