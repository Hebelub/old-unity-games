using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    // public KeyCode letter;
    public string letter;
    public Player player;
    public GameObject gameObject;

    private Vector3Int atCoord;

    // private Vector3Int[] checkerOffsets = { new Vector3Int(0, 2, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 0, 0), new Vector3Int(1, -1, 0), new Vector3Int(0, -2, 0), new Vector3Int(-1, -1, 0), new Vector3Int(-2, 0, 0), new Vector3Int(-1, 1, 0) };
    // private Vector3Int atCoord;

    // private GameManager gm = GameManager.instance;

    // private string possibleLetters = "abcdefghijklmnopqrstuvwxyz0123456789";

    // It should also add itselves to the array

    public Tile(/* Vector3Int atCoord,*/ GameObject go, string letter, Vector3Int atCoord)
    {
        #region SomeMovedCode
        // this.atCoord = atCoord;

        //bool satisfyed = false;

        //int random = 0;

        //while (!satisfyed)
        //{
        //    random = Random.Range(0, 36);

        //    satisfyed = true;

        //    letter = GetKeyCodeNumber(random);

        //    Tile compareToTile = null;

        //    for (int i = 0; i < checkerOffsets.Length; i++)
        //    {
        //        compareToTile = gm.GetTile(checkerOffsets[i] + atCoord);

        //        if (compareToTile!= null && compareToTile.letter == letter)
        //        {
        //            satisfyed = false;
        //            break;
        //        }
        //    }

        //}
        #endregion

        this.letter = letter;
        gameObject = go;
        this.atCoord = atCoord;
        // gm.SetTile(atCoord, this);
    }

    #region Obsolete
    //private string GetKeyCodeNumber(int number)
    //{
    //    return possibleLetters[number].ToString();

    //    
    //    //switch (number)
    //    //{
    //    //    case 0:
    //    //        return KeyCode.A;
    //    //    case 1:
    //    //        return KeyCode.B;
    //    //    case 2:
    //    //        return KeyCode.C;
    //    //    case 3:
    //    //        return KeyCode.D;
    //    //    case 4:
    //    //        return KeyCode.E;
    //    //    case 5:
    //    //        return KeyCode.F;
    //    //    case 6:
    //    //        return KeyCode.G;
    //    //    case 7:
    //    //        return KeyCode.H;
    //    //    case 8:
    //    //        return KeyCode.I;
    //    //    case 9:
    //    //        return KeyCode.J;
    //    //    case 10:
    //    //        return KeyCode.K;
    //    //    case 11:
    //    //        return KeyCode.L;
    //    //    case 12:
    //    //        return KeyCode.M;
    //    //    case 13:
    //    //        return KeyCode.N;
    //    //    case 14:
    //    //        return KeyCode.O;
    //    //    case 15:
    //    //        return KeyCode.P;
    //    //    case 16:
    //    //        return KeyCode.Q;
    //    //    case 17:
    //    //        return KeyCode.R;
    //    //    case 18:
    //    //        return KeyCode.S;
    //    //    case 19:
    //    //        return KeyCode.T;
    //    //    case 20:
    //    //        return KeyCode.U;
    //    //    case 21:
    //    //        return KeyCode.V;
    //    //    case 22:
    //    //        return KeyCode.W;
    //    //    case 23:
    //    //        return KeyCode.X;
    //    //    case 24:
    //    //        return KeyCode.Y;
    //    //    case 25:
    //    //        return KeyCode.Z;
    //    //    case 26:
    //    //        return KeyCode.Alpha0;
    //    //    case 27:
    //    //        return KeyCode.Alpha1;
    //    //    case 28:
    //    //        return KeyCode.Alpha2;
    //    //    case 29:
    //    //        return KeyCode.Alpha3;
    //    //    case 30:
    //    //        return KeyCode.Alpha4;
    //    //    case 31:
    //    //        return KeyCode.Alpha5;
    //    //    case 32:
    //    //        return KeyCode.Alpha6;
    //    //    case 33:
    //    //        return KeyCode.Alpha7;
    //    //    case 34:
    //    //        return KeyCode.Alpha8;
    //    //    case 35:
    //    //        return KeyCode.Alpha9;
    //    //    default:
    //    //        return new KeyCode();
    //    //}
    //    
    //}
    #endregion

    // Should be called if a player lands or exits on this tile
    public void PlayerEnter(Player newPlayer)
    {
        // The letter is replaced as soon as you land on it
        GameManager.instance.CmdReplaceLetterAt(atCoord);
        // Player is sat to a new player
        player = newPlayer;

        //// Should kill player currently standing on the tile
        //if (player != null)
        //{
        //    player.Die(newPlayer);

        //}

        // Should remove the letter variable so that noone can walk onto it

        // [?] Should also remove current letter and give player a score value of some sort
    }
    public void PlayerExit()
    {
        // Sets the player to null
        player = null;

    }

}

public class Power
{
    public string activation;

}

/*
 * [?] IDEA:
 * You have a color
 * All letters you transform will get your color
 * The more colors you have, the moreincome you get
 * If you move from your color, the letter will not be replaced
 */
