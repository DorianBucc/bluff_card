using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager instance;
    public List<CardTypeData> stack = new();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }

    public void UpdateStack(List<CardTypeData> cards)
    {
        stack = new List<CardTypeData>(cards);
    }
}
