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

    public void InitializePlayers(int playerCount)
    {
        if (playerCount <= 0)
        {
            return;
        }

        players.Clear();

        for (int i = 0; i < playerCount; i++)
        {
            string playerName = $"Player {i + 1}";

            players.Add(new Player(playerName, new List<Card>()));
        }

        // Pour la première manche, le joueur qui commence est choisi aléatoirement
        playerIndex = Random.Range(0, players.Count);

        currentPlayer = players[playerIndex];

        CanvasManager.instance.UpdatePlayerName(currentPlayer.name);
    }

    public void DealPlayersCards(List<Card> cards, int cardsPerPlayer)
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

    public void RemoveCurrentPlayerCards(List<Card> cards)
    {
        currentPlayer.RemoveCards(cards);
    }

    public void ClearPlayersHands()
    {
        foreach (Player player in players)
        {
            player.ClearCards();
        }
    }

    public int GetPlayerCount()
    {
        return players.Count;
    }

    public Player GetWinner()
    {
        return players.Count > 0 ? players[0] : null;
    }

    public Player GetNextPlayer(Player player)
    {
        int index = players.IndexOf(player);

        if (index == -1) return null;

        int nextPlayerIndex = (index + 1) % players.Count;

        return players[nextPlayerIndex];
    }

    public Player GetPreviousPlayer()
    {
        int playerCount = GetPlayerCount();
        int previousPlayerIndex = (playerIndex - 1 + playerCount) % playerCount;

        return players[previousPlayerIndex];
    }

    public bool IsLastPlayer()
    {
        int playersWithCardsCount = 0;

        foreach (Player p in players)
        {
            if (p.cards.Count > 0)
            {
                playersWithCardsCount++;
            }
        }

        return playersWithCardsCount == 1;
    }

    public void NextPlayer()
    {   
        SwitchToNextPlayer();

        if (IsLastPlayer())
        {
            GameManager.instance.CallBluff();
            return;
        }

        CanvasManager canvasManager = CanvasManager.instance;

        canvasManager.DisplayHand(currentPlayer.cards);
        canvasManager.UpdatePlayerName(currentPlayer.name);
        canvasManager.UpdatePlayerLife(currentPlayer);
    }

    public void RemovePlayer(Player player)
    {
        if (players.Remove(player))
        {
            SyncCurrentPlayer();
        }
    }

    public void SetCurrentPlayer(Player player)
    {
        int index = players.IndexOf(player);

        if (index != -1)
        {
            playerIndex = index;

            SyncCurrentPlayer();
        }
    }

    private void SwitchToNextPlayer()
    {
        playerIndex++;

        SyncCurrentPlayer();
    }

    private void SyncCurrentPlayer()
    {
        if (players.Count > 0)
        {
            playerIndex %= players.Count;
            currentPlayer = players[playerIndex];
        }
        else
        {
            currentPlayer = null;
        }
    }

    // private void PlayRandom(Player player)
    // {
    //     List<Card> playerCards = player.cards;

    //     int playerCardCount = playerCards.Count;

    //     if (playerCardCount <= 1)
    //     {   
    //         GameManager.instance.CallBluff();
    //         return;
    //     }

    //     int randomIndex = Random.Range(0, playerCardCount);

    //     Card randomCard = playerCards[randomIndex];

    //     CardManager cardManager = CardManager.instance;

    //     cardManager.AddSelectedCard(randomCard);
    //     cardManager.ConfirmSelectedCard();
    // }
}
