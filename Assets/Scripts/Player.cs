using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public bool isAI;
    public string name;
    public List<Card> cards;
    public Revolver revolver;
    public bool isDead;

    public Player(string name, List<Card> cards) {
        isAI = false;
        this.name = name;
        this.cards = cards;
        revolver = new Revolver();
        isDead = false;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void RemoveCards(List<Card> listCard)
    {
        foreach (Card card in listCard)
        {
            cards.Remove(card);
        }
    }

    public void ClearCards()
    {
        cards.Clear();
    }

    public void TakeDamage()
    {
        if (revolver.PullTrigger())
        {
            isDead = true;
        }
    }
}
