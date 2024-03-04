using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    public int frame = 0;
    public float speedOfTime = 1f;      public float speedOfTimeRemember = 1f;
    public bool isSpooling = false;

    public KeyCode spoolBackwards;
    public KeyCode spoolForwards;
    public KeyCode startAfterSpooling;
    public KeyCode setToFirstFrame;

    public KeyCode fasterSpeed;
    public KeyCode slowerSpeed;

    public float saveFrameFrequency = 1f;

    public List<TrackRevind> trackedObjects;

    private void Start()
    {
        StartCoroutine(ISaveFrames());

        ChangeSpeedOfTime(speedOfTime);

    }

    private void Update()
    {
        if (Input.GetKeyDown(spoolForwards))
        {
            Spool(true);
            ChangeFrame(1);
        }
        if (Input.GetKeyDown(spoolBackwards))
        {
            Spool(true);
            ChangeFrame(-1);
        }
        if (Input.GetKeyDown(startAfterSpooling))
        {
            Spool(false);
        }
        if (Input.GetKeyDown(setToFirstFrame))
        {
            ChangeFrame(-frame);
        }

        if (Input.GetKeyDown(fasterSpeed))
        {
            ChangeSpeedOfTime(speedOfTime + 0.25f);
        }
        if (Input.GetKeyDown(slowerSpeed))
        {
            ChangeSpeedOfTime(speedOfTime - 0.25f);
        }
    }

    public IEnumerator ISaveFrames()
    {
        while(!isSpooling)
        {
            SaveFrame();
            ChangeFrame(1);

            if(isSpooling)
            {
                break;
            }

            yield return new WaitForSeconds(saveFrameFrequency / speedOfTime);
        }
    }

    public void ChangeSpeedOfTime(float newSpeed)
    {
        speedOfTime = newSpeed;
        speedOfTimeRemember = speedOfTime;
    }

    public void Spool (bool spool)
    {
        isSpooling = spool;
        
        if(spool)
        {
            // speedOfTimeRemember = speedOfTime;
            speedOfTime = 0f;
        }
        else if (!spool)
        {
            speedOfTime = speedOfTimeRemember;
            StartCoroutine(ISaveFrames());
        }

    }

    public void SaveFrame()
    {
        foreach (TrackRevind trackedObject in trackedObjects)
        {
            trackedObject.SaveOneFrame();
        }
    }

    public void ChangeFrame(int delta)
    {

        frame += delta;
     
        if (frame < 0)
        {
            frame = 0;
        }

        //else if (trackedObjects[0].transformHistory.Count < frame)
        //{
        //    frame = trackedObjects[0].transformHistory.Count - 1;
        //}

        if(isSpooling)
        {
            foreach (TrackRevind trackedObject in trackedObjects)
            {
                trackedObject.LoadFrameNr(frame);
            }
        }

    }
    public void SetFrame(int frame)
    {
        this.frame = frame;
    }

}