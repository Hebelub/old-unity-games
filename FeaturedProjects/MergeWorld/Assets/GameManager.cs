﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public AudioSource audioSource;

    public RecipeMaker recipeMaker;

    public int idCount = 0;

}

//[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
//public class Item : ScriptableObject
//{
//    public Entity obj;
//}