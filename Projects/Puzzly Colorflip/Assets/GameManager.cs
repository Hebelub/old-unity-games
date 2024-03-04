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

    }
    #endregion

    public Shape[] spriteShapes;

    public List<Unit> allUnits;

}

[CreateAssetMenu(fileName = "New Shape", menuName = "Shape", order = 1)]
public class Shape : ScriptableObject
{
    public string shapeName;
    public Sprite shape;
    public string method;
    public string methodDescription;
}