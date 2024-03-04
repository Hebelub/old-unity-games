using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Letter", menuName = "Letter")]
public class Letter : ScriptableObject {

    public KeyCode keyCode;

    public int width;

    public int centerAt;

    public bool[] shape;

}

[CreateAssetMenu(fileName = "New Font", menuName = "Font")]
public class Font : ScriptableObject
{
    public bool noCapitalLetters;

    public Letter[] lowerCaseLetters;
    public Letter[] upperCaseLetters;

    public Font ()
    {
        if (noCapitalLetters)
        {
            lowerCaseLetters = upperCaseLetters;
        }
    }

}

[CreateAssetMenu(fileName = "New Tile", menuName = "Obstacle")]
public class Obstacle : ScriptableObject
{

    public bool blocks;
    public GameObject gameObject;

}