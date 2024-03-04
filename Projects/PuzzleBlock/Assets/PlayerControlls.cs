using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    public GameObject spot;

    public List<Font> fonts;
    public Font selectedFont;

    public List<GameObject> spots;

    public bool upperCase;

    public void ChangeToLetter (Letter letter)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Get start vector
        Vector3 startVector = new Vector3(letter.centerAt % letter.width, 0, -(int)Mathf.Floor(letter.centerAt / letter.width));

        

        for (int i = 0; i < letter.shape.Length; i++)
        {
            if (letter.shape[i])
            {
                Vector3 position = new Vector3(i % letter.width, 0, -(int)Mathf.Floor(i / letter.width)) - startVector;
                Instantiate(spot, position + transform.position, Quaternion.identity, transform);

                

            }
        }

    }

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Letter[] letters;

        if (upperCase)
        {
            letters = selectedFont.upperCaseLetters;
        }
        else
        {
            letters = selectedFont.lowerCaseLetters;
        }

		foreach(Letter letter in letters)
        {
            if (Input.GetKeyDown(letter.keyCode))
            {
                Debug.Log(Input.anyKeyDown);
                ChangeToLetter(letter);
            }
        }

        // Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v));

	}
}

// e T Y U I O p a D f H J K L X C V