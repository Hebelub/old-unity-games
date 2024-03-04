using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lists : MonoBehaviour {

    public int ifAmmount;
    public int variableAmmount;
    public int functionAmmount;
    public int conditionAmmount;
    public int actionAmmount;

    public List<If> ifs;
    public List<Variable> v;
    public List<Function> f;
    public List<Condition> c;
    public List<Action> a;

    private void Start()
    {
        
    }

    private void CreateBrain()
    {
        for (int i = 0; i < ifAmmount; i++)
        {
            RandomIf();
        }
        for (int i = 0; i < ifAmmount; i++)
        {
            RandomIf();
        }
    }

    private void RandomVariable()
    {
        float number = R(0, 20);
        Variable variable = new Variable(number);
        v.Add(variable);
    }
    private void RandomCondition()
    {
        Condition condition = new Condition(v[R(0, v.Count)], R(0,8), v[R(0, v.Count)]);
        c.Add(condition);
    }
    private void RandomAction()
    {

    }
    private void RandomIf()
    {

    }

    private int R(int min, int max)
    {
        return (int)Random.Range(min, max);
    }
}
