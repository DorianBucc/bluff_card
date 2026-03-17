using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private int playerIndex = 0;
    public Player currentPlayer;
    public List<Player> players = new();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public int GetPlayerCount()
    {
        return players.Count;
    }

    public void NextTurn()
    {   
        playerIndex++;
        playerIndex %= GetPlayerCount();

        currentPlayer = players[playerIndex];
        currentPlayer.NewTurn();
        
        // prend le prochain joueur
        // joueur.startRound()
    }

    public void NextRound()
    {
        
    }
    
    public void RemovePreviousPlayer()
    {
        
    }

    public void RemoveCards(List<Card> listCard)
    {
        currentPlayer.RemoveCard(listCard);
    }

}
