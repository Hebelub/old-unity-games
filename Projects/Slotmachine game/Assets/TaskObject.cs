using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskObject : MonoBehaviour
{

    public Transform spriteHolderObject;
    public TextMeshProUGUI text;

    public void AddSprite(Task task)
    {
        Instantiate(task.symbol, spriteHolderObject.position, Quaternion.identity, spriteHolderObject);
        task.ammountText = text;
    }
}
