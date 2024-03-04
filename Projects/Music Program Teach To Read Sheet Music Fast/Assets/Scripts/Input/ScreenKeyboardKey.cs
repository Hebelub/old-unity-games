using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKeyboardKey : MonoBehaviour
{
    private int octave = 0;

    public int pitch = 0; // A pitch of 0 is the middle-c
    public SpriteRenderer spriteRenderer;

    public Color normalColor = Color.white;
    public Color hoverColor = Color.white;

    public bool pressed = false;

    private void Start()
    {
        octave = GetComponentInParent<ScreenKeyboardOctave>().octave;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = hoverColor;
    }
    private void OnMouseExit()
    {
        spriteRenderer.color = normalColor;
    }

    private void OnMouseDown()
    {
        Pressed(pitch, 2.0f);
        pressed = true;
    }
    private void OnMouseUp()
    {
        if(pressed == true)
        {
            Pressed(pitch, -1.0f);
            pressed = false;
        }
    }

    public void Pressed(int pitch, float v)
    {
        // GameManager.instance.instrumentInput.KeyPressed(pitch + octave * M.Octave);

        InstrumentInput.inputNotes[pitch] = v;
    }
}
