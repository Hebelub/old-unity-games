using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable
{
    public float variable;

    public Variable(float variable)
    {
        this.variable = variable;
    }
}

public class If
{

    Condition condition;
    Function function;

    public If(Condition condition, Function function)
    {
        this.condition = condition;
        this.function = function;
    }


}

public class Function
{

    public List<Action> actions;

    public Function (List<Action> actions)
    {
        this.actions = actions;
    }

}

public class Condition
{

    Variable var1;
    int check;
    Variable var2;

    public Condition(Variable var1, int check, Variable var2)
    {
        this.var1 = var1;
        this.check = check;
        this.var2 = var2;
    }

    public bool Calculate()
    {
        float var1 = this.var1.variable;
        float var2 = this.var2.variable;
        switch (check)
        {
            case 0:
                if (var1 == var2) return true; return false;
            case 1:
                if (var1 != var2) return true; return false;
            case 2:
                if (var1 < var2) return true; return false;
            case 3:
                if (var1 > var2) return true; return false;
            case 4:
                if (var1 <= var2) return true; return false;
            case 5:
                if (var1 >= var2) return true; return false;
            case 6:
                return true;
            case 7:
                return false;
            default:
                Debug.LogWarning("Operation do not exist"); return false;
        }
    }
}

public class Action
{

    // Possible actions

}

public static class Operation
{

    public static int equals = 0;
    public static int not = 1;
    public static int greater = 2;
    public static int less = 3;
    public static int greaterOrEqual = 4;
    public static int lessOrEqual = 5;
    public static int always = 6;
    public static int never = 7;

}