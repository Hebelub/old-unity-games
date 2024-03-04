using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackRevind : MonoBehaviour
{
    public Rigidbody rb;

    public List<SavedFrame> transformHistory = new List<SavedFrame>();
    // public List<Rigidbody> rigidbodyHistory = new List<Rigidbody>();

    private void Start()
    {
        GameManager.instance.trackedObjects.Add(this);
        rb = GetComponent<Rigidbody>();
    }

    public void LoadFrameNr(int frame)
    {
        transform.position = transformHistory[frame].position;
        transform.rotation = transformHistory[frame].rotation;
        transform.localScale = transformHistory[frame].scale;

        // rb.velocity = rigidbodyHistory[frame].velocity;
        // rb.angularVelocity = rigidbodyHistory[frame].angularVelocity;
    }

    public void SaveOneFrame()
    {
        int currentFrame = GameManager.instance.frame;
        int numberOfFrames = transformHistory.Count;

        if(currentFrame < numberOfFrames)
        {
            transformHistory[currentFrame] = new SavedFrame(transform.position, transform.rotation, transform.localScale);
        }
        else
        {
            // --> Must fix so that the list is removed when you revind and stuff
            // transformHistory.Splice(currentFrame, transformHistory.Count);
            // rigidbodyHistory.Splice(currentFrame, transformHistory.Count);

            transformHistory.Add(new SavedFrame(transform.position, transform.rotation, transform.localScale));
        }

    }

}

public class SavedFrame
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public SavedFrame(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }
}
