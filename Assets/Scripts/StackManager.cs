using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public static StackManager instance;
    public List<Card> stack = new();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }

    public void UpdateStack(List<Card> cards)
    {
        stack = new List<Card>(cards);
    }

    public bool CheckValidStack(CardData symbolCardRound)
    {
        foreach (Card card in stack)
        {
            if (card.data != symbolCardRound)
                return false;
        }

        return true;
    }
}
