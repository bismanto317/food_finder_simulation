using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListVariable<T> : ScriptableObject
{
    public bool ResetValueWhenPlay;
    public List<T> Items = new List<T>();

    private void OnEnable()
    {
        if(ResetValueWhenPlay)
        {
            Items = new List<T>();
        }
    }
    public void Add(T t)
    {
        if (!Items.Contains(t)) Items.Add(t);
    }

    public void Remove(T t)
    {
        if (Items.Contains(t)) Items.Remove(t);
    }
}
