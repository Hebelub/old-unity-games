using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentInput : MonoBehaviour
{
    public static float[] inputNotes; // This is the glue of all inputs

    GameManager gm;

    MidiInput midiInput;

    public bool inputMidi = true;
    public bool inputScreenPiano = false;
    public bool inputComputerKeyboard = false;

    private void Start()
    {
        gm = GameManager.instance;
        midiInput = GetComponent<MidiInput>();
        
    }

    private void Update()
    {

        if (inputComputerKeyboard) KeyboardInput();
        if (inputMidi) CheckMidiInput();

    }

    public void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 9).x] = 2; else if (Input.GetKeyUp(KeyCode.A)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 9).x] = -1;
        if (Input.GetKeyDown(KeyCode.S)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 8).x] = 2; else if (Input.GetKeyUp(KeyCode.S)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 8).x] = -1;
        if (Input.GetKeyDown(KeyCode.D)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 7).x] = 2; else if (Input.GetKeyUp(KeyCode.D)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 7).x] = -1;
        if (Input.GetKeyDown(KeyCode.F)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 6).x] = 2; else if (Input.GetKeyUp(KeyCode.F)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 6).x] = -1;
        if (Input.GetKeyDown(KeyCode.G)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 5).x] = 2; else if (Input.GetKeyUp(KeyCode.G)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 5).x] = -1;
        if (Input.GetKeyDown(KeyCode.H)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 4).x] = 2; else if (Input.GetKeyUp(KeyCode.H)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 4).x] = -1;
        if (Input.GetKeyDown(KeyCode.J)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 3).x] = 2; else if (Input.GetKeyUp(KeyCode.J)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 3).x] = -1;
        if (Input.GetKeyDown(KeyCode.K)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 2).x] = 2; else if (Input.GetKeyUp(KeyCode.K)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 2).x] = -1;
        if (Input.GetKeyDown(KeyCode.L)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 1).x] = 2; else if (Input.GetKeyUp(KeyCode.L)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC - 1).x] = -1;
                                                                                                                                                                                                     
        if (Input.GetKeyDown(KeyCode.Q)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 0).x] = 2; else if (Input.GetKeyUp(KeyCode.Q)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 0).x] = -1;
        if (Input.GetKeyDown(KeyCode.W)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 1).x] = 2; else if (Input.GetKeyUp(KeyCode.W)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 1).x] = -1;
        if (Input.GetKeyDown(KeyCode.E)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 2).x] = 2; else if (Input.GetKeyUp(KeyCode.E)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 2).x] = -1;
        if (Input.GetKeyDown(KeyCode.R)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 3).x] = 2; else if (Input.GetKeyUp(KeyCode.R)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 3).x] = -1;
        if (Input.GetKeyDown(KeyCode.T)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 4).x] = 2; else if (Input.GetKeyUp(KeyCode.T)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 4).x] = -1;
        if (Input.GetKeyDown(KeyCode.Y)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 5).x] = 2; else if (Input.GetKeyUp(KeyCode.Y)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 5).x] = -1;
        if (Input.GetKeyDown(KeyCode.U)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 6).x] = 2; else if (Input.GetKeyUp(KeyCode.U)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 6).x] = -1;
        if (Input.GetKeyDown(KeyCode.I)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 7).x] = 2; else if (Input.GetKeyUp(KeyCode.I)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 7).x] = -1;
        if (Input.GetKeyDown(KeyCode.O)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 8).x] = 2; else if (Input.GetKeyUp(KeyCode.O)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 8).x] = -1;
        if (Input.GetKeyDown(KeyCode.P)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 9).x] = 2; else if (Input.GetKeyUp(KeyCode.P)) inputNotes[M.TranslatePitchToPlacement(M.MiddleC + 9).x] = -1;
    }
    public void CheckMidiInput()
    {

        for (int i = 0; i < M.AllNotes; i++)
        {
            if (MidiInput.GetKeyDown(i))
            {
                Debug.Log(i - M.MiddleC);
                // Debug.Log(i - 60 + " is pressed!");
                KeyPressed(i - M.MiddleC);
            }
        }
    }

    public void AddToList(bool up, float velocity)
    {
        
    }

    public void KeyPressed(int keyPressed)
    {

        foreach (Note note in C.notesInPlayArea)
        {
            if (note.pitch == keyPressed)
            {
                note.Hit();
                return;
            }
            else
            {
                // ShowPress(false, keyPressed);
                // note.nrOfmisses++;
            }
        }

        //foreach (Note note in toLateNotes)
        //{
        //    note.Miss();
        //}

        ShowPress(false, keyPressed);
    }

    public static void ShowPress(bool didHit, int pitch)
    {
        if (C.notesInPlayArea.Count <= 0) return;

        Note noteWithSmallestDistanceToPitch = null;

        foreach (Note note in C.notesInPlayArea)
        {
            if (noteWithSmallestDistanceToPitch == null)
            {
                noteWithSmallestDistanceToPitch = note;
                break;
            }
            if (DistanceToNote(note) < DistanceToNote(noteWithSmallestDistanceToPitch))
            {
                noteWithSmallestDistanceToPitch = note;
            }
            int DistanceToNote(Note testNote)
            {
                return Mathf.Abs(testNote.pitch - pitch);
            }
        }

        NoteLine noteLine = noteWithSmallestDistanceToPitch.noteLine;

        float noteDistance = noteLine.noteDistance;

        float yPosition = noteLine.GetYPositionOfPlacement(M.TranslatePitchToPlacement(pitch).x);

        Vector3 position = Vector3.up * yPosition - Vector3.right * 15f;

        GameObject showPressed = Instantiate(GameManager.instance.showPressedNotePrefab, position, Quaternion.identity);

        showPressed.GetComponentInChildren<ShowPressedNote>().didHit = didHit;

        GameManager.instance.StartCoroutine(IDisapear());

        IEnumerator IDisapear()
        {
            while(true)
            {
                Debug.Log("Dette er en test");

                if (MidiInput.GetKeyUp(pitch + M.MiddleC))
                {
                    Destroy(showPressed);
                }

                yield return null;
            }
        }
    }

}
