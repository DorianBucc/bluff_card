using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager instance;
    public List<CardTypeData> stack;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }
}
