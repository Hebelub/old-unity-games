using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform startPosision;
    public Transform canvas;

    public List<Task> tasks;
    public List<Objective> objectives;
                                                            
    public Objective currentObjective;

    private void Start()
    {
        List<Task> t = new List<Task>();
        Task task = tasks[0];
        task.aimAmmount = 7;
        t.Add(task);
        task.aimAmmount = 5;
        t.Add(task);
        t.Add(task);
        t.Add(task);
        t.Add(task);
        t.Add(task);
        t.Add(task);
        currentObjective = new Objective(t);

        currentObjective.DisplayObjective();
    }
}
