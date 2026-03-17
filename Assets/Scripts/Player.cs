using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Card> cards = new();

    public void NewTurn()
    {
        CardUIManager.instance.LoadPlayerDeck(cards);
    } // Envoyer les cartes cardUIManager singleton

    public void RemoveCard(List<Card> listCard)
    {
        foreach (Card card in listCard)
        {
            cards.Remove(card);
        }
    }
}
