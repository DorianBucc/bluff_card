using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<CardTypeData> cards = new();

    public void NewTurn()
    {
        
    } // Envoyer les cartes cardUIManager singleton

    public void RemoveCard(List<CardTypeData> listCard)
    {
        foreach (CardTypeData card in listCard)
        {
            cards.Remove(card);
        }
    }
}
