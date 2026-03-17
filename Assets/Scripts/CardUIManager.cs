using System.Collections.Generic;
using UnityEngine;

public class CardUIManager : MonoBehaviour
{
    static public CardUIManager instance;
    public List<CardUI> deck = new();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadPlayerDeck(List<Card> cards)
    {
        int index = 0;

        foreach (Card card in cards)
        {
            CardUI cardUI = deck[index];

            cardUI.card = card;
            cardUI.Show();
            
            index++;
        }
        
    }
}
