using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class M
{
    #region Constants
    public const int Octave = 12;
    public const int Scale = 7;
    public const int AllNotes = 128;
    public const int MiddleC = 60;

    #endregion

    #region Scales and stuff 
    
    #endregion

    #region GameObjects and stuff
    // Declared in the GameManager
    public static Cleff cleffG;
    public static Cleff cleffF;

    public static GameObject doubleFlat;
    public static GameObject singleFlat;
    public static GameObject natural;
    public static GameObject singleSharp;
    public static GameObject doubleSharp;

    // Note parts
    public static GameObject filledNoteHead;
    public static GameObject emptyNoteHead;
    public static GameObject noteStem;
    public static GameObject noteFlag;
    #endregion

    #region KeySignature

    public static int[] GetPlacementOfAccidentals(int key, Cleff cleff)
    {
        int[] flat = new int[]
        {
            // 6, 2, 5, 1, 4, 0, 3
            6, 9, 5, 8, 4, 7, 3
        };
        int[] sharp = new int[]
        {
            // 3, 0, 4, 1, 5, 2, 6
            10, 7, 11, 8, 5, 9, 6
        };

        int octaveDown = 0;
        if (cleff.centerFromC > 0) octaveDown = M.Scale * 2;

        int[] returner = new int[Mathf.Abs(key)];
        int[] flatOrSharp;

        if (key > 0)
        {
            flatOrSharp = sharp;
        }
        else
        {
            flatOrSharp = flat;
        }

        for (int i = 0; i < Mathf.Abs(key); i++)
        {
            returner[i] = flatOrSharp[i] - octaveDown;
        }

        return returner;
    }

    public static int[] GetScaleOfKey(int key)
    {
        int[][] flatScales =
        new int[][]
        {
                new int[7] { 0, 2, 4, 5, 7, 9, 11 },
                new int[7] { 0, 2, 4, 5, 7, 9, +10 },
                new int[7] { 0, 2, +3, 5, 7, 9, 10 },
                new int[7] { 0, 2, 3, 5, 7, +8, 10 },
                new int[7] { 0, +1, 3, 5, 7, 8, 10 },
                new int[7] { 0, 1, 3, 5, +6, 8, 10 },
                new int[7] { 11, 1, 3, 5, 6, 8, 10 },
                new int[7] { 11, 1, 3, +4, 6, 8, 10 }
        };

        int[][] sharpScales =
            new int[][]
            {
                new int[7] { 0, 2, 4, 5, 7, 9, 11 },
                new int[7] { 0, 2, 4, +6, 7, 9, 11 },
                new int[7] { +1, 2, 4, 6, 7, 9, 11 },
                new int[7] { 1, 2, 4, 6, +8, 9, 11 },
                new int[7] { 1, +3, 4, 6, 8, 9, 11 },
                new int[7] { 1, 3, 4, 6, 8, +10, 11 },
                new int[7] { 1, 3, +5, 6, 8, 10, 11 },
                new int[7] { 1, 3, 5, 6, 8, 10, +0 }
            };

        if (key > 0)
            return sharpScales[Mathf.Abs(key)];
        else
            return flatScales[Mathf.Abs(key)];
    }

    public static Vector2Int TranslatePitchToPlacement(int pitch /*, should also take the key signature*/)
    {
        int testPitch = pitch;

        int yPlacement = 0;

        bool didWork = false;

        while (didWork == false)
        {
            for (int i = 0; i < M.Scale; i++)
            {
                if (M.Mod(testPitch, M.Octave) == M.GetScaleOfKey(C.CurrentKeySignature)[i])
                {
                    yPlacement = i + M.OctaveOnNote(testPitch, M.Octave) * M.Scale;
                    didWork = true;
                    break;
                }
            }

            if (didWork == false)
            {
                int keySignature = C.CurrentKeySignature;
                if (keySignature == 0) keySignature++;
                testPitch += keySignature / Mathf.Abs(keySignature);
            }
        }

        return new Vector2Int(yPlacement, testPitch - pitch);
        // Return.x = what placement
        // Return.y = what accidental it had to add
    }
    public static int TranslatePlacementToPitch(int placement)
    {
        int inList = Mod(placement, M.Scale);

        int returner = M.GetScaleOfKey(C.CurrentKeySignature)[inList] + Mathf.FloorToInt(placement / M.Scale) * M.Octave;

        return returner;
    }
    #endregion

    #region MusicGenerator
    public static void SpawnNotes()
    {
        GameManager.instance.StartCoroutine(ISpawnNotes(Staff.noteLines[0], new Vector2Int(0, 4)));
        GameManager.instance.StartCoroutine(ISpawnNotes(Staff.noteLines[1], new Vector2Int(-4, 0)));

        IEnumerator ISpawnNotes(NoteLine noteLine, Vector2Int range)
        {
            while (true)
            {
                Note spawnedNote = null;

                // if (Random.value < 0.5f)
                    spawnedNote = GenerateRandomNote(noteLine, range);

                C.spawningAtCurrentBar += 1f; // ---> Should be different here but just add one bare for every note now

                yield return new WaitForSeconds(C.secondsPerWholeNote * spawnedNote.value);
            }
        }
    }


    public static Note GenerateRandomNote(NoteLine noteLine, Vector2Int range)
    {
        Note previousNote = null;
        if (C.commingNotes.Count > 0)
        {
            previousNote = C.commingNotes[C.commingNotes.Count - 1];
        }

        //float goldenPointOffset = M.CalculateGoldenPointOffset();

        Note storedNote = new Note(RandomNote(), previousNote, noteLine, 1f / Mathf.Pow(2, Random.Range(0, 5)), C.spawningAtCurrentBar, 0f);

        //for (int i = 0; i < 2; i++)
        //{
        //    Note testingNote = new Note(RandomNote(), previousNote, noteLine, 1f, C.spawningAtCurrentBar, 0f);

        //    if (goldenPointOffset <= 0 && testingNote.expectedTiming < storedNote.expectedTiming)
        //    {
        //        storedNote = testingNote;
        //    }
        //    else if (testingNote.expectedTiming > storedNote.expectedTiming)
        //    {
        //        storedNote = testingNote;
        //    }
        //}

        /*storedNote.gameObject =*/ // NoteSpawner.SpawnNote(storedNote, noteLine);
        storedNote.Spawn();

        return storedNote;

        int RandomNote()
        {
            int octaveDrops = 0;
            int random = Random.Range(range.x, range.y);

            while (random > M.Scale)
            {
                octaveDrops += 1;
                random -= M.Scale;
            }
            while (random < 0)
            {
                octaveDrops -= 1;
                random += M.Scale;
            }
            int randomNote = M.GetScaleOfKey(C.CurrentKeySignature)[M.Mod(random, M.Scale)] + octaveDrops * M.Octave;
            // Debug.Log("Random % 7: " + random % 7 + ", Random: " + random + ", OctaveDrops: " + octaveDrops + ", Random note: " + randomNote);
            return randomNote;
        }

    }

    // public int[] cMajor = new int[7];
    #endregion

    #region DifficultyTracker
    public static List<Note> playedNotes = new List<Note>();

    public static float GetExpectedTime(Note note)
    {
        float expextedTime = 1f;

        for (int i = playedNotes.Count - 1; i > 0; i--)
        {
            Note foundNote = playedNotes[i];
            if (foundNote.pitch == note.pitch)
            {
                expextedTime = foundNote.timeUsed;

                if (note.previousNote != null && foundNote.previousNote != null && note.previousNote.pitch == foundNote.previousNote.pitch)
                {
                    expextedTime = foundNote.timeUsed;
                    note.moreExactExpectation = true;
                    break;
                }
            }
        }

        return expextedTime;
    }

    public static float CalculateGoldenPointOffset()
    {
        if (C.commingNotes.Count > 0)
            return -GameManager.instance.goldenPoint.position.z + C.commingNotes[0].gameObject.transform.position.x;
        else return 0f;
    }
    #endregion

    #region Random Stuff
    public static int Mod(float a, float b)
    {
        return (int) (a - b * Mathf.Floor(a / b));
    }
    public static int OctaveOnNote(int note, int notesInScale)
    {
        // Something is a little bit wrong here :)

        if (note < 0)
        {
            note = note - notesInScale + 1;
        }

        return Mathf.FloorToInt(note / notesInScale);
    }

    public static int NumberOfSupportinglinesNeededOnNote(Note note, Cleff cleff)
    {
        int numberOfSupportingLines = 0;

        int placement = M.TranslatePitchToPlacement(note.pitch).x;
        int relativeToCenter = placement += cleff.centerFromC;
        if (Mathf.Abs(relativeToCenter) > 5)
        {
            if (relativeToCenter < 0) relativeToCenter += 4;
            else relativeToCenter -= 4;

            numberOfSupportingLines = Mathf.FloorToInt(relativeToCenter / 2);
        }

        return numberOfSupportingLines;
    }
    #endregion
}

public class Cleff
{
    public GameObject gameObject; // Spawn on this one
    public int centerFromC = 6; // How many placements the center line is from the middle C

    public Cleff(GameObject gameObject, int centerFromC)
    {
        this.gameObject = gameObject;
        this.centerFromC = centerFromC;
    }

}