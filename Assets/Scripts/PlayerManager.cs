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
        Player nextPlayer = SwitchToNextPlayer();

        if (nextPlayer.isAI)
        {
            AIManager.instance.PlayRandom(nextPlayer);
        } 
        else
        {
            CardUIManager.instance.DisplayDeck(nextPlayer.cards);
        }
    }

    private Player SwitchToNextPlayer()
    {
        playerIndex = (playerIndex + 1) % GetPlayerCount();

        currentPlayer = players[playerIndex];

        return currentPlayer;
    }

    public void RemoveCards(List<Card> listCard)
    {
        currentPlayer.RemoveCards(listCard);
    }
}
