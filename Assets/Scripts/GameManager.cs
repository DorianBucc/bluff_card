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
    public int currentRound = 0;
    public int currentTurnInRound = 0;
    public bool isGameOver = false;
    public List<StackOfCardType> cardStacks;
    public List<CardData> targetedCards;
    private CardData currentTargetedCard;
    public Image ImageTargetedCard;
    public TextMeshProUGUI TextDebug;
    public Animator animatorTurn;

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
        currentRound = 0;
        currentTurnInRound = 0;
        isGameOver = false;

        PlayerManager.instance.InitializePlayers(playerCount);

        SetupRound();
        CanvasManager canvasManager = CanvasManager.instance;
        canvasManager.SetNumberOfCardsPlayedPreviousTurn(
                currentTargetedCard.cardName
            );      
    }

    private void SetupRound()
    {

        PlayerManager playerManager = PlayerManager.instance;
        CanvasManager canvasManager = CanvasManager.instance;

        currentRound++;
        currentTurnInRound = 1;

        List<Card> cards = InitializeCards();

        StackManager.instance.ClearStack();

        playerManager.ClearPlayersHands();
        playerManager.DealPlayersCards(cards, cardsPerPlayer);
        canvasManager.HideStack();
        canvasManager.UpdatePlayerName(playerManager.currentPlayer.name);
        canvasManager.UpdatePlayerLife(playerManager.currentPlayer);
        canvasManager.DisplayHand(playerManager.currentPlayer.cards);

        InitializeTargetedCard();

        canvasManager.SetTargetedCardText(currentTargetedCard.cardName);
        canvasManager.SetNumberOfCardsPlayedPreviousTurn(currentTargetedCard.cardName);
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


    public void EndTurn()
    {
        if (isGameOver) return;

        CardManager cardManager = CardManager.instance;

        if (cardManager.selectedCards.Count <= 0)
        {
            CanvasManager.instance.ShakeHand();
            return;
        }
        cardManager.ConfirmSelectedCard(() =>
        {
            animatorTurn.enabled = true;
        });
    }
    public void NextTurn()
    {
        PlayerManager playerManager = PlayerManager.instance;
        CanvasManager canvasManager = CanvasManager.instance;

        playerManager.NextPlayer();

        canvasManager.SetNumberOfCardsPlayedPreviousTurn(
            currentTargetedCard.cardName
        );

        currentTurnInRound++;
        
    }

    public void NextRound()
    {
        StartCoroutine(NextRoundRoutine());
    }

    private IEnumerator NextRoundRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        TextDebug.text = string.Empty;

        SetupRound();
    }

    public void CallBluff()
    {
        if (isGameOver) return;

        StackManager stackManager = StackManager.instance;

        // Impossible de call un bluff si aucune carte n'a encore été posée
        if (stackManager.stack.Count == 0) return;

        PlayerManager playerManager = PlayerManager.instance;

        Player accuser = playerManager.currentPlayer;
        Player accused = playerManager.GetPreviousPlayer();

        bool isHonest = stackManager.CheckValidStack(currentTargetedCard);

        Player loser = isHonest ? accuser : accused;
        
        loser.TakeDamage();

        if (loser.isDead)
        {
            Player nextPlayer = playerManager.GetNextPlayer(loser);

            playerManager.RemovePlayer(loser);

            // En cas de victoire
            if (playerManager.GetPlayerCount() <= 1)
            {
                isGameOver = true;

                CanvasManager canvasManager = CanvasManager.instance;

                Player winner = playerManager.GetWinner();

                TextDebug.text = $"{loser.name} died ! {winner.name} won !";

                canvasManager.UpdatePlayerName(winner.name);
                canvasManager.HideHand();
                
                return;
            }

            TextDebug.text = $"{loser.name} died !";

            // Si le perdant meurt, le joueur suivant commence le prochain tour
            playerManager.SetCurrentPlayer(nextPlayer);
        }
        else
        {
            TextDebug.text = $"{loser.name} lost... and he survived !";

            // Si le perdant survit, il commence le prochain tour
            playerManager.SetCurrentPlayer(loser);
        }

        NextRound();
    }
}
