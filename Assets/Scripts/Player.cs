using System.Collections.Generic;

public class Player
{
    public List<Card> cards;

    public Player() {
        cards = new();
    }

    public Player(List<Card> cards) {
        this.cards = cards;
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
