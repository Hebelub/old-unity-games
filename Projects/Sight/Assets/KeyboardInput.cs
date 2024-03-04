using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private void Update()
    {
        DetectKeyboardInput();
    }

    public KeyCode[] inputs;

    public void DetectKeyboardInput()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            if (Input.GetKeyDown(inputs[i]))
            {
                Debug.Log("Pressed " + i);
                GameManager.instance.CreateRandomNote();
            }
        }
    }

}
