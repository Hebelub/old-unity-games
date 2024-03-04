using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class C // Current play
{
    #region Current Play
    // Things like dynamics, keysignatre, timeSignature should all be in a list of when things changes. The changes should be put into the list of changes and when they are put into action.

    public static float secondsPerWholeNote = 4.4f; // How much time it should use every whole note
    public static float noteMoveSpeed = 1f; // How far it moves every second

    public static float spawningAtCurrentBar = 0f; // Must be attached to the music generator // How many seconds into the music is spawned right now

    public static List<Note> commingNotes = new List<Note>();
    public static List<Note> notesInPlayArea = new List<Note>();

    public static List<Change> changes = new List<Change>();

    private static int currentKeySignature = 0;
    public static int CurrentKeySignature
    {
        get
        {
            return currentKeySignature;
        }
        set
        {
            currentKeySignature = value;
            UpdateKeySignature();
        }
    }

    private static void UpdateKeySignature()
    {
        foreach (NoteLine noteLine in Staff.noteLines)
        {
            noteLine.SetKeySignature();
        }

        foreach (Note note in C.commingNotes)
        {
            NoteSpawner.AddAccidentalsToNote(note);
        }
    }

    #endregion
}

public class Change
{
    public float atWhatTime = 0f;

    public GameObject gameObject;

    public Change ()
    {
        atWhatTime = C.spawningAtCurrentBar;
    }
}

public class ChangeKeySignature : Change
{
    public int keySignature = 0;


    public ChangeKeySignature(int keySignature)
    {
        this.keySignature = keySignature;
    }
}
public class ChangeDynamics : Change
{
    public float volume; // How hard to press, a value of 0 should be any value

    public ChangeDynamics(float volume)
    {
        this.volume = volume;
    }
}
public class ChangePedaling : Change
{
    public Pedaling pedaling;
    public bool down = true;
    public float howMuch = 0f; // If it is zero it does not matter

    public ChangePedaling(Pedaling pedaling, bool down)
    {
        this.pedaling = pedaling;
        this.down = down;
    }

    public enum Pedaling
    {
        softPedal,
        sostenutoPedal,
        sustainPedal
    }

}
// It should be more changing things down here ---> . . .