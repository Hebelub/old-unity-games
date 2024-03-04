using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputArea : MonoBehaviour
{
    #region Singelton
    public static InputArea instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private float beginning;
    public float Beginning
    {
        get
        {
            CalibrateBeginning();
            return beginning;
        }
        set
        {
            transform.position = new Vector3((End + value) / 2, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(End - value, transform.localScale.y, transform.localScale.z);
            // beginning = value;
        }
    }

    private float end;
    public float End
    {
        get
        {
            CalibrateEnd();
            return end;
        }
        set
        {
            transform.position = new Vector3((value + Beginning) / 2, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(value - Beginning, transform.localScale.y, transform.localScale.z);
            // end = value;
        }
    }

    private void CalibrateBeginning()
    {
        beginning = transform.position.x - transform.localScale.x / 2;
    }
    private void CalibrateEnd()
    {
        end = transform.position.x + transform.localScale.x / 2;
    }

    private void Start()
    {
        CalibrateBeginning();
        CalibrateEnd();

        Beginning = -10;
        End = -6;
    }

}
