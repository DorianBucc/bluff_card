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
        playerIndex = (playerIndex + 1) % GetPlayerCount();

        currentPlayer = players[playerIndex];
        
        CardUIManager.instance.DisplayDeck(currentPlayer.cards);
    }

    public void RemovePreviousPlayer()
    {
        
    }

    public void RemoveCards(List<Card> listCard)
    {
        currentPlayer.RemoveCards(listCard);
    }
}
