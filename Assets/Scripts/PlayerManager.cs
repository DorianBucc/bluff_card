using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<Player> players = new();
    public Player currentPlayer;
    private int playerIndex = 0;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void InitializePlayers(List<Player> players)
    {
        if (players == null || players.Count <= 0)
        {
            return;
        }

        this.players = new List<Player>(players);

        playerIndex = 0;

        currentPlayer = this.players[0];
        CanvasManager.instance.UpdatePlayerName(currentPlayer.name);
    }

    public int GetPlayerCount()
    {
        return players.Count;
    }

    public Player GetPreviousPlayer()
    {
        int playerCount = GetPlayerCount();

        int previousPlayerIndex = (playerIndex - 1 + playerCount) % playerCount;

        return players[previousPlayerIndex];
    }

    public void DealCards(List<Card> cards, int cardsPerPlayer)
    {
        int cardIndex = 0;

        foreach (Player player in players)
        {
            for (int i = 0; i < cardsPerPlayer; i++)
            {
                player.AddCard(cards[cardIndex]);
                
                cardIndex++;
            }
        }
    }

    public void NextPlayer()
    {   
        SwitchToNextPlayer();

        if (currentPlayer.isAI)
        {
            PlayRandom(currentPlayer);
        } 
        else
        {
            CanvasManager.instance.DisplayHand(currentPlayer.cards);
        }
        
        CanvasManager.instance.UpdatePlayerName(currentPlayer.name);
    }

    private void SwitchToNextPlayer()
    {
        playerIndex = (playerIndex + 1) % GetPlayerCount();

        currentPlayer = players[playerIndex];
    }

    private void PlayRandom(Player player)
    {
        List<Card> playerCards = player.cards;

        int playerCardCount = playerCards.Count;

        if (playerCardCount <= 1)
        {   
            GameManager.instance.CallBluff();
            return;
        }

        int randomIndex = Random.Range(0, playerCardCount);

        Card randomCard = playerCards[randomIndex];

        print("IA card: " + randomCard.data.cardName);

        CardManager cardManager = CardManager.instance;

        cardManager.AddSelectedCard(randomCard);
        cardManager.ConfirmSelectedCard();

        PlayerManager.instance.NextPlayer();
    }

    public void RemoveCards(List<Card> listCard)
    {
        currentPlayer.RemoveCards(listCard);
    }
}
