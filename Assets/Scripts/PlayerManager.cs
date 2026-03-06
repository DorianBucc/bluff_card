using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private int indexPlayer = 0;
    private int CountPlayer;
    public Player currentPlayer;
    public List<Player> listPlayers = new();

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // CountPlayer = listPlayers.Count;
    }
    public void nextGameTurn()// Tour suivant
    {   
        indexPlayer++;
        indexPlayer = indexPlayer%CountPlayer;
        currentPlayer = listPlayers[indexPlayer];
        currentPlayer.newTurn();
        // prend le prochain joueur
        // joueur.startRound()
    }
    public void nextRound() // Nouvelle ou prochaine manche
    {
        
    }
    public void removePreviousPlayer()
    {
        
    }

    public void removeCards(List<CardTypeData> listCard)
    {
        currentPlayer.RemoveCard(listCard);
    }

}
