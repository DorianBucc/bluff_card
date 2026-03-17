using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private int playerIndex = 0;
    public Player currentPlayer;
    public List<Player> listPlayers = new();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        // CountPlayer = listPlayers.Count;
    }

    public int GetPlayerCount()
    {
        return listPlayers.Count;
    }

    public void NextGameTurn() // Tour suivant
    {   
        playerIndex++;
        playerIndex %= GetPlayerCount();

        currentPlayer = listPlayers[playerIndex];
        currentPlayer.NewTurn();
        
        // prend le prochain joueur
        // joueur.startRound()
    }
    public void NextRound() // Manche suivante
    {
        
    }
    public void RemovePreviousPlayer()
    {
        
    }

    public void RemoveCards(List<CardTypeData> listCard)
    {
        currentPlayer.RemoveCard(listCard);
    }

}
