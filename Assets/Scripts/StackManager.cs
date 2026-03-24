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

    public bool checkValidStack(CardTypeData symbolCardRound)
    {
        foreach (var card in stack)
        {
            if(card.typeData.cardName != symbolCardRound.cardName)
                return false;
        }
        return true;
    }
}
