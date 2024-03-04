using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterToReveal : MonoBehaviour
{
    public string hiddenLetter;
    string inputKeyLetter;

    public TextMeshProUGUI text;

    private void Start()
    {

    }

    public void UpdateHiddenLetter(string newLetter)
    {
        hiddenLetter = newLetter.ToUpper().Trim();
        inputKeyLetter = hiddenLetter.ToLower().Trim();
    }


    private void Update()
    {
        if(Input.GetKeyDown(inputKeyLetter))
        {
            text.text = hiddenLetter;
        }
    }


}
