using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// !!!!!!!! --->>>--->>> This could be in the NoteLine itselves to place it on that spesific note line
public class NoteSpawner : MonoBehaviour
{
    //public static void SpawnNote(Note note, NoteLine noteLine)
    //{
    //    // if (noteLine == Staff.noteLines[0]) Debug.Log("Number of supporting lines: " + supportingLines);

    //    note.indexInList = C.commingNotes.Count;

    //    C.commingNotes.Add(note);

    //    float yPosition = noteLine.GetYPositionOfPlacement(M.TranslatePitchToPlacement(note.pitch).x);

    //    Vector3 spawnPosition = new Vector3(10, yPosition, 0);
    //    GameObject noteGameObject = Instantiate(GameManager.instance.notePrefab, spawnPosition, Quaternion.identity, GameManager.instance.notesFolder);

    //    note.gameObject = noteGameObject;

    //    AddSupportingLinesToNote(note, noteLine);
    //    AddAccidentalsToNote(note);
    //}

    static public void AddAccidentalsToNote(Note note)
    {
        // This does not yet support natural signs though :(

        note.accidental = M.TranslatePitchToPlacement(note.pitch).y;

        if (note.accidentalGo != null)
            Destroy(note.accidentalGo);

        if (note.accidental == 0) return;

        Vector3 relativeToNote = -0.23f * Vector3.right;
        Vector3 spawnPosition = relativeToNote + note.gameObject.transform.position;

        GameObject accidentalGo = null;

        if (note.accidental == -1) accidentalGo = M.singleSharp;
        else if (note.accidental == 1) accidentalGo = M.singleFlat;
        else if (note.accidental < -1) accidentalGo = M.doubleSharp;
        else if (note.accidental > 1) accidentalGo = M.doubleFlat;

        GameObject go = Instantiate(accidentalGo, spawnPosition, Quaternion.identity);
        go.transform.parent = note.gameObject.transform;

        note.accidentalGo = go;
    }

    static public void AddSupportingLinesToNote(Note note, NoteLine noteLine)
    {
        int supportingLines = M.NumberOfSupportinglinesNeededOnNote(note, noteLine.cleff);

        for (int i = 0; i < Mathf.Abs(supportingLines); i++)
        {
            int multiplayer = 0;

            if (supportingLines < 0)
            {
                multiplayer = -1;
            }
            else if (supportingLines > 0)
            {
                multiplayer = 1;
            }

            float yPosition = noteLine.GetYPositionOfPlacement(multiplayer * i * 2 - noteLine.cleff.centerFromC + 6 * multiplayer);

            Vector3 position = new Vector3(10, yPosition, 0);

            GameObject go = Instantiate(GameManager.instance.supportingLinePrefab, position, Quaternion.identity, note.gameObject.transform);
        }

    }

    private void Update()
    {
        if (C.commingNotes.Count > 0) C.commingNotes[0].timeUsed += Time.deltaTime;
    }

}
