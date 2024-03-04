using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(MoveNotes());
    }

    public Transform notesTransform;
    public GameObject noteHead;

    public List<Note> notes;

    // Should be stored in something else
    public float currentTempo; // In bpm

    public IEnumerator MoveNotes()
    {
        while(true)
        {
            notesTransform.Translate(new Vector3(-currentTempo, 0));

            yield return null;
        }
    }

    public void CreateRandomNote()
    {
        GameObject go = Instantiate(noteHead, Vector3.zero, Quaternion.identity, notesTransform);

        Note note = new Note(Random.Range(-9, 10), go);

        // Must also put it into the list, this might also be placed inside the construction of the Note class!
    }
}

public class Note
{
    public int pitch; // Relative to zero
    public GameObject gameObject; // The instantiated instance of this note    

    public Note(int pitch, GameObject gameObject)
    {
        this.pitch = pitch;
        this.gameObject = gameObject;

    }
}