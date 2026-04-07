using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public bool isAI;
    public string name;
    public List<Card> cards;
    public int bulletsInChamber;
    public int maxChambers;
    public bool isDead;

    public Player(string name, List<Card> cards) {
        isAI = false;
        this.name = name;
        this.cards = cards;
        bulletsInChamber = 1;
        maxChambers = 6;
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

    public bool PullTrigger()
    {
        int roll = Random.Range(1, maxChambers + 1);

        if (roll <= bulletsInChamber)
        {
            isDead = true;

            return true;
        }

        IncreaseDanger();

        return false;
    }

    private void IncreaseDanger()
    {
        if (bulletsInChamber < maxChambers)
        {
            bulletsInChamber++;
        }
    }
}
