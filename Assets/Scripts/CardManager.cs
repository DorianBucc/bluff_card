using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<Card> selectedCards = new();
    public int maxSelectableCards = 3;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }

    public bool CanSelectMore()
    {
        return selectedCards.Count < maxSelectableCards;
    }

    public void AddSelectedCard(Card card)
    {
        if (!selectedCards.Contains(card))
        {
            selectedCards.Add(card);
        }
    }

    public void RemoveSelectedCard(Card card)
    {
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
        }
    }

    public void ConfirmSelectedCard()
    {
        CanvasManager canvasManager = CanvasManager.instance;

        StackManager.instance.UpdateStack(selectedCards);
        PlayerManager.instance.RemoveCards(selectedCards);

        canvasManager.UnselectHand();
        canvasManager.HideHand();

        selectedCards.Clear();
    }
}
