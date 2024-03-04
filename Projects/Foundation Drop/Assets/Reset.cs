﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            ResetLevel();
        }
	}

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
