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

    public int GetNumberOfCardsInStack()
    {
        return stack.Count;
    }

    public void UpdateStack(List<Card> cards)
    {
        stack = new List<Card>(cards);
    }

    public void ClearStack()
    {
        stack.Clear();
    }

    public bool CheckValidStack(CardData currentCardTargeted)
    {
        foreach (Card card in stack)
        {
            if (card.data != currentCardTargeted || card.data.cardName == "Joker")
                return false;
        }

        return true;
    }
}
