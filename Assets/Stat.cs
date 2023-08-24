using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]    //make var editable in inspector
    private int baseValue;

    [SerializeField]
    private List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);    //for each x in modifiers. Add x onto final value
        return finalValue;
    }

    public int SetBaseValue(int value){
        baseValue = value; 
        return baseValue;
    }

    public void AddModifier (int modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveMovidier(int modifier)
    {
        if(modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
