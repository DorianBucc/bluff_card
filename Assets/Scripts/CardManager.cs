using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<Card> selectedCards = new();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }

    public void AddSelectedCard(Card card)
    {
        selectedCards.Add(card);
    }

    public void RemoveSelectedCard(Card card)
    {
        selectedCards.Remove(card);
    }

    public void ConfirmSelectedCard()
    {
        StackManager.instance.UpdateStack(selectedCards);
        PlayerManager.instance.RemoveCards(selectedCards);
    }
}
