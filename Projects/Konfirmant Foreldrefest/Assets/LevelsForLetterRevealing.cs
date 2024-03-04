using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelsForLetterRevealing : MonoBehaviour
{
    public string[] levels;
    int currentLevel = 0;

    private void Start()
    {
        SpawnLevel(currentLevel);
    }

    public void Nextlevel()
    {
        SpawnLevel(currentLevel + 1);
        currentLevel += 1;
        foreach(TextMeshProUGUI child in transform.GetComponentsInChildren<TextMeshProUGUI>())
        {
            child.text = "";
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Nextlevel();
        }
    }

    public void SpawnLevel(int level)
    {
        string strLevel = levels[level % levels.Length];

        for(int i = 0; i < strLevel.Length; i++)
        {
            char c = strLevel[i];
            transform.GetChild(i).GetComponent<LetterToReveal>().UpdateHiddenLetter(c.ToString());
        }
    }
}
