using System.Collections.Generic;

public class Player
{
    public bool isAI;
    public string name;
    public List<Card> cards;

    public Player() {
        isAI = false;
        cards = new();
    }

    public Player(List<Card> cards, bool isAI) {
        this.cards = cards;
        this.isAI = isAI;
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
}
