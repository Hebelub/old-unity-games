using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    #region Singelton
    public static Shoot instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    public float speed;
    public GameObject ball;
    public Ball nextShot;

    private void Start()
    {
        Activate();
    }

    private void Update()
    {
        if(nextShot != null)
        {
            nextShot.transform.position = Mouse();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            nextShot.Send();
            nextShot = null;
        }

    }

    Vector3 Mouse()
    {
        var v3 = Input.mousePosition;
        v3.z += 2;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;
    }

    public void Activate()
    {
        nextShot = Instantiate(ball, Mouse(), Quaternion.identity).GetComponent<Ball>();
    }

}
