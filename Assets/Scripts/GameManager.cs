using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerCount = 2;
    public int cardsPerPlayer = 5;
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
        InitializeGame();
    }

    private void InitializeGame()
    {
        PlayerManager.instance.InitializePlayers(playerCount);

        SetupRound();
    }

    private void SetupRound()
    {
        PlayerManager playerManager = PlayerManager.instance;
        List<Card> cards = InitializeCards();

        playerManager.ClearPlayersHands();
        StackManager.instance.ClearStack();

        playerManager.DealCards(cards, cardsPerPlayer);

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
        StartCoroutine(NextRoundRoutine());
    }

    // Temporaire
    private IEnumerator NextRoundRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        TextDebug.text = string.Empty;

        SetupRound();
    }

    public void CallBluff()
    {
        PlayerManager playerManager = PlayerManager.instance;

        Player accuser = playerManager.currentPlayer;
        Player accused = playerManager.GetPreviousPlayer();

        bool isHonest = StackManager.instance.CheckValidStack(currentTargetedCard);

        Player loser = isHonest ? accuser : accused;

        loser.TakeDamage();

        if (loser.isDead)
        {
            TextDebug.text = $"{loser.name} lost and died !";
        }
        else
        {
            TextDebug.text = $"{loser.name} lost but survived !";
        }

        NextRound();
    }
}
