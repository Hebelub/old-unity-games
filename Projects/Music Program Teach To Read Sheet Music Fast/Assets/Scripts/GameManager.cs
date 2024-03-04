using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        // Declering the cleffs and accidentals in M    <--- This should be in another script, also the variables where you decare the GameObjects <<<---<<<---
        M.cleffG = new Cleff(goCleffG, -6);
        M.cleffF = new Cleff(goCleffF, 6);

        M.doubleFlat = doubleFlat;
        M.singleFlat = singleFlat;
        M.natural = natural;
        M.singleSharp = singleSharp;
        M.doubleSharp = doubleSharp;

        M.filledNoteHead = filledNoteHead;
        M.emptyNoteHead = emptyNoteHead;
        M.noteStem = noteStem;
        M.noteFlag = noteFlag;
    }
    #endregion

    public NoteSpawner noteSpawner;
    public InstrumentInput instrumentInput;
    [Space(10)]
    // public NoteLine[] noteLines;
    [Space(10)]
    public Transform goldenPoint;
    public Transform notesFolder;
    [Space(10)]
    // public GameObject notePrefab;
    public GameObject supportingLinePrefab;
    public GameObject showPressedNotePrefab;
    [Space(10)]
    // public float secondsEachBar = 1f;
    // public float barsEverySecond = 1f;

    public GameObject goCleffG; // Theese should be moved to a separate script so not make so much garbage in the GameManager
    public GameObject goCleffF;
    public GameObject doubleFlat;
    public GameObject singleFlat;
    public GameObject natural;
    public GameObject singleSharp;
    public GameObject doubleSharp;

    // Note parts
    public GameObject filledNoteHead;
    public GameObject emptyNoteHead;
    public GameObject noteStem;
    public GameObject noteFlag;

    private void Start()
    {
        // Starting to spawn notes
        M.SpawnNotes();
    }

    private void Update()
    {
        int invertor = 1;
        if (C.CurrentKeySignature < 0)
        {
            invertor = -1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) C.CurrentKeySignature = 0 * invertor;
        if (Input.GetKeyDown(KeyCode.Alpha1)) C.CurrentKeySignature = 1 * invertor;
        if (Input.GetKeyDown(KeyCode.Alpha2)) C.CurrentKeySignature = 2 * invertor;
        if (Input.GetKeyDown(KeyCode.Alpha3)) C.CurrentKeySignature = 3 * invertor;
        if (Input.GetKeyDown(KeyCode.Alpha4)) C.CurrentKeySignature = 4 * invertor;
        if (Input.GetKeyDown(KeyCode.Alpha5)) C.CurrentKeySignature = 5 * invertor;
        if (Input.GetKeyDown(KeyCode.Alpha6)) C.CurrentKeySignature = 6 * invertor;
        if (Input.GetKeyDown(KeyCode.Alpha7)) C.CurrentKeySignature = 7 * invertor;

        if (Input.GetKeyDown(KeyCode.Minus)) C.CurrentKeySignature *= -1;

    }

    //public void DestroyGo(GameObject go)
    //{
    //    Destroy(go);
    //}

    public Gradient scoreGradient;

}

public class Note
{
    public int pitch;
    public GameObject gameObject;
    public float expectedTiming;
    public Note previousNote; // ---> You should remove this since you can access it by indexInList

    public NoteLine noteLine;

    public bool moreExactExpectation = false; // Just temerarly

    public float timeUsed = 0; // Starts calculation when it is the next note
    public int nrOfmisses; // Adds one every miss

    public int accidental = 0;
    public GameObject accidentalGo;

    public float value = 1f; // It tells for how many bars to hold the key
    public float playAtTime = 0f; // It tells when to start hitting the key

    public float volume = 0f; // It tells how hard you should press the key. If it is zero you can press whatever volume you want

    public int indexInList; // What position of the C.commingNotes it is on

    public float hitScore; // How good you hit the note
    public Location location; // What list it is stored in

    public float pressedLength; // For how long the note has been pressed // 0 means that it has not yet been pressed

    private bool noteDisabled = false;
    public bool noteHit = false;

    public Note (int pitch, Note previousNote, NoteLine attachedToNoteLine, float value, float playAtTime, float volume)
    {
        this.pitch = pitch;
        this.previousNote = previousNote;
        noteLine = attachedToNoteLine;
        expectedTiming = M.GetExpectedTime(this);
        // gameObject = GameManager.instance.noteSpawner.SpawnNote(this);

        this.value = value;
        this.playAtTime = playAtTime;

        this.volume = volume;

        location = Location.CommingNotes;
    }

    public enum Location
    {
        CommingNotes,
        NotesInPlayArea,
        PlayedNotes
    }

    public void Spawn()
    {
        this.indexInList = C.commingNotes.Count;
        C.commingNotes.Add(this);
        //ChangeNoteLocation(Location.CommingNotes);

        float yPosition = noteLine.GetYPositionOfPlacement(M.TranslatePitchToPlacement(this.pitch).x);

        GameObject headGo = M.filledNoteHead; if (value >= 0.5f) headGo = M.emptyNoteHead;
        Vector3 spawnPosition = new Vector3(10, yPosition, 0);
        gameObject = Object.Instantiate(headGo, spawnPosition, Quaternion.identity, GameManager.instance.notesFolder);

        NoteSpawner.AddSupportingLinesToNote(this, noteLine);
        NoteSpawner.AddAccidentalsToNote(this);

        if (C.commingNotes.Count <= 1)
        {
            DetectEnterArea();
            
        }
    }

    public void Hit()
    {
        if (pressedLength > 0) return;

        noteHit = true;

        // ChangeNoteLocation(Location.PlayedNotes);

        GameManager.instance.StartCoroutine(ICheckPress());

        // Show the pressed note
        InstrumentInput.ShowPress(true, pitch);

        IEnumerator ICheckPress()
        {
            while (!MidiInput.GetKeyUp(pitch + M.MiddleC) && !noteDisabled)
            {
                yield return null;

                pressedLength += Time.deltaTime;

                hitScore = 1f / (Mathf.Abs(pressedLength - value) + 1);
                UpdateColor();
            }

            Disable();
        }
    }
    public void Miss()
    {
        hitScore = 0f;
        UpdateColor();
        Disable();
    }

    public void ChangeNoteLocation (Location location)
    {
        if (this.location == location) return;

        void RemoveNoteFromCurrentLocation()
        {
            if (this.location == Location.CommingNotes)
            {
                // Should remove it from the C.commingNotes list
                C.commingNotes.RemoveAt(indexInList);

                // Change index of all notes after the one you removed
                for (int i = indexInList; i < C.commingNotes.Count; i++)
                {
                    C.commingNotes[i].indexInList--;
                }
            }
            else if (this.location == Location.NotesInPlayArea)
            {
                C.notesInPlayArea.RemoveAt(indexInList);

                for (int i = indexInList; i < C.notesInPlayArea.Count; i++)
                {
                    C.notesInPlayArea[i].indexInList--;
                }
            }
            else if (this.location == Location.PlayedNotes)
            {
                M.playedNotes.RemoveAt(indexInList);

                for (int i = indexInList; i < M.playedNotes.Count; i++)
                {
                    M.playedNotes[i].indexInList--;
                }
            }
        }

        RemoveNoteFromCurrentLocation();

        this.location = location;

        if (location == Location.CommingNotes)
        {
            C.commingNotes.Add(this);
            indexInList = C.commingNotes.Count - 1;
        }
        else if (location == Location.NotesInPlayArea)
        {
            C.notesInPlayArea.Add(this);
            indexInList = C.notesInPlayArea.Count - 1;
        }
        else if (location == Location.PlayedNotes)
        {
            M.playedNotes.Add(this);
            indexInList = M.playedNotes.Count - 1;
        }
    }

    public void UpdateColor()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = GameManager.instance.scoreGradient.Evaluate(hitScore);
    }

    public void Disable()
    {
        ChangeNoteLocation(Location.PlayedNotes);

        noteDisabled = true;

        // Turn the gameObject grey to show that it is disabled
        // --> It should turn to the collour that coresponds how well you hit the note
        // gameObject.GetComponentInChildren<SpriteRenderer>().color = GameManager.instance.scoreGradient.Evaluate(hitScore);

        // Should destroy the gameObject
        Object.Destroy(gameObject, 20f);
    }

    private bool isDetectingExit = false;

    public void DetectEnterArea()
    {
        if (Detect()) return;

        GameManager.instance.StartCoroutine(IDetect());

        IEnumerator IDetect()
        {
            while (!noteDisabled)
            {
                yield return null;

                if (Detect()) break;
            }
        }

        // returns whether or not the note is inside the InputArea
        bool Detect()
        {
            if (gameObject.transform.position.x > InputArea.instance.Beginning &&
                gameObject.transform.position.x < InputArea.instance.End &&
                !noteHit)
            {
                // Changes location
                ChangeNoteLocation(Location.NotesInPlayArea);
                // Tells the next note to detect
                if (C.commingNotes.Count > 0)
                    C.commingNotes[0].DetectEnterArea();
                if (isDetectingExit == false)
                    DetectExitArea();

                return true;
            }
            else return false;
        }
    }
    public void DetectExitArea()
    {
        if (Detect()) return;

        isDetectingExit = true;

        GameManager.instance.StartCoroutine(IDetect());

        IEnumerator IDetect()
        {
            while (!noteDisabled && !noteHit)
            {
                yield return null;

                if (Detect()) break;
            }
        }

        // returns whether or not the note is inside the InputArea
        bool Detect()
        {
            if (gameObject.transform.position.x < InputArea.instance.Beginning)
            {
                // Changes location
                Miss();
                //ChangeNoteLocation(Location.PlayedNotes);

                // Tells the next note to detect
                if (C.notesInPlayArea.Count > 0) C.notesInPlayArea[0].DetectExitArea();
                else isDetectingExit = false;

                return true;
            }
            else return false;
        }
    }
}

// Things to do:

    /*
     * Make a way to have all keysignatures
        * Maby an array with current signature, and a function that takes int as parameter
          that int shows how many b's or #'s there are. # is posetive and b is negative.
          Add int[][] to store more key signatures, or calculate it for every time.
          It should also get the placements of the b's and #'s and be able to place it on
          as a key signature.
     * Add barlines
     * Different note values
     * Get audio of the played note
     * Make it the program change difficulty in real time acording to how good you are reading notes
        * Make the program be better predicting how much time you use on aspesific note
     * Input the lowest and the highest pich on the keyboard to calibrate the program to this inputsource.
       The program should not spawn notes wich is ouside of this range. And it could also save some 
       preformance
     * 
     * Gjøre at om man trykker på en senere note, så blir de før feil.
     * Gjøre at man sjekker hvor bra noten ble trykket.
     * 
     */