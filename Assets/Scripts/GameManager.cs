using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerCount = 2;
    public List<CardTypeSlot> cardTypeSlots;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        List<Player> players = new();

        for (int i = 0; i < playerCount; i++)
        {
            Player player = new();

            players.Add(player);
        }

        List<Card> cards = InitializeCards();

        PlayerManager playerManager = PlayerManager.instance;

        playerManager.InitializePlayers(players);

        playerManager.DealCards(cards, 5);

        CardUIManager.instance.DisplayDeck(playerManager.currentPlayer.cards);
    }

    private List<Card> InitializeCards()
    {
        List<Card> cards = new();

        int id = 1;

        foreach (var cardTypeSlot in cardTypeSlots)
        {
            for (int i = 0; i < cardTypeSlot.quantity; i++)
            {
                Card card = new(id, cardTypeSlot.cardType);

                cards.Add(card);

                id++;
            }
        }

        return ShuffleCards(cards);
    }

    public void NextTurn()
    {
        CardManager.instance.ConfirmSelectedCard();
    }

    private List<Card> ShuffleCards(List<Card> cardsToShuffle)
    {
        List<Card> shuffledCards = new(cardsToShuffle);

        for (int i = shuffledCards.Count - 1; i >= 0 ; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            (shuffledCards[randomIndex], shuffledCards[i]) = (shuffledCards[i], shuffledCards[randomIndex]);
        }

        return shuffledCards;
    }
}
