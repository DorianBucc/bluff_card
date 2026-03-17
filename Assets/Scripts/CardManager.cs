using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<CardTypeData> selectedCards = new();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }

    public void AddSelectedCard(CardTypeData card)
    {
        selectedCards.Add(card);
    }

    public void RemoveSelectedCard(CardTypeData card)
    {
        selectedCards.Remove(card);
    }

    public void ConfirmCardSelected()
    {
        StackManager.instance.UpdateStack(selectedCards);
        PlayerManager.instance.RemoveCards(selectedCards);
    }
}
