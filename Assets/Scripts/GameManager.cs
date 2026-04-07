using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerCount = 2;
    public List<StackOfCardType> cardStacks;
    public List<CardData> targetedCards;
    private CardData currentTargetedCard;
    public Image ImageTargetedCard;
    public TextMeshProUGUI TextDebug;    

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        List<Player> players = new()
        {
            new("Player", new List<Card>()),
            new("Player2", new List<Card>())
        };

        List<Card> cards = InitializeCards();

        PlayerManager playerManager = PlayerManager.instance;

        playerManager.InitializePlayers(players);

        playerManager.DealCards(cards, 5);

        InitializeTargetedCard();

        CanvasManager.instance.DisplayHand(playerManager.currentPlayer.cards);
    }

    private List<Card> InitializeCards()
    {
        List<Card> cards = new();

        int id = 1;

        foreach (StackOfCardType cardStack in cardStacks)
        {
            for (int i = 0; i < cardStack.quantity; i++)
            {
                Card card = new(id, cardStack.data);

                cards.Add(card);

                id++;
            }
        }

        cards.Shuffle(); 

        return cards;
    }

    public void InitializeTargetedCard()
    {
        currentTargetedCard = targetedCards[Random.Range(0, targetedCards.Count)];
        ImageTargetedCard.sprite = currentTargetedCard.sprite;
    }

    public void NextTurn()
    {
        CardManager cardManager = CardManager.instance;

        if (cardManager.selectedCards.Count <= 0)
        {
            CanvasManager.instance.ShakeHand();
            return;
        }

        cardManager.ConfirmSelectedCard();

        PlayerManager.instance.NextPlayer();
    }

    public void NextRound()
    {
       // TODO
    }

    public void CallBluff()
    {
        PlayerManager playerManager = PlayerManager.instance;

        Player accuser = playerManager.currentPlayer;
        Player accused = playerManager.GetPreviousPlayer();

        bool isHonest = StackManager.instance.CheckValidStack(currentTargetedCard);

        if (isHonest)
        {
            TextDebug.text = $"{accuser.name} lost";
            // playerCalling.Eliminate()
        }
        else
        {
            TextDebug.text = $"{accused.name} lost";
            // playerTarget.Eliminate()
        }

        // Malus appeler pour le perdant à créer
    }
}
